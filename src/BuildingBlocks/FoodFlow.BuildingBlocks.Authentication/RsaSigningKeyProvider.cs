using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FoodFlow.BuildingBlocks.Authentication;

public sealed class RsaSigningKeyProvider : IDisposable
{
    private readonly RSA _rsa;

    public RsaSigningKeyProvider(IOptions<JwtOptions> options)
    {
        ArgumentNullException.ThrowIfNull(options);
        var jwtOptions = options.Value;

        _rsa = RSA.Create(2048);
        if (!string.IsNullOrEmpty(jwtOptions.PrivateKeyPem))
        {
            _rsa.ImportFromPem(jwtOptions.PrivateKeyPem);
        }

        SecurityKey = new RsaSecurityKey(_rsa) { KeyId = jwtOptions.KeyId };
        SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.RsaSha256);
    }

    public RsaSecurityKey SecurityKey { get; }

    public SigningCredentials SigningCredentials { get; }

    public JsonWebKey GetPublicJsonWebKey()
    {
        var publicKey = new RsaSecurityKey(_rsa.ExportParameters(includePrivateParameters: false))
        {
            KeyId = SecurityKey.KeyId
        };

        var jwk = JsonWebKeyConverter.ConvertFromRSASecurityKey(publicKey);
        jwk.Use = "sig";
        jwk.Alg = SecurityAlgorithms.RsaSha256;
        return jwk;
    }

    public void Dispose() => _rsa.Dispose();
}
