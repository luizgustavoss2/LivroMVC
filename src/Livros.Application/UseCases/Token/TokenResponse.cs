using Livros.Application.Notifications;
using Livros.Domain.Entities;

namespace Livros.Application.UseCases
{
    public class TokenResponse : ResponseBase
    {
        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public static explicit operator TokenResponse(Token token)
        {
            return new TokenResponse
            {
                Authenticated = token.Authenticated,
                Created = token.Created,
                Expiration = token.Expiration,
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken
            };
        }
    }
}
