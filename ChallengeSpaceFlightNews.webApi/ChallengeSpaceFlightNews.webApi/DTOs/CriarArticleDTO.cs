using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeSpaceFlightNews.webApi.DTOs
{
    public class CriarArticleDTO : Notifiable<Notification>
    {
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string ImageUrl { get; set; }
        public string NewsSite { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public bool Featured { get; set; } = false;

        public void Validar()
        {
            AddNotifications(
                new Contract<Notification>()
                .Requires()
                .IsNotEmpty(Title, "Title", "O titulo não pode estar vazio")
                .IsNotEmpty(Url, "Url", "A url não pode estar vazia")
                .IsNotEmpty(NewsSite, "NewsSite", "O nome do site não pode estar vazio")
                .IsNotEmpty(Summary, "Summary", "O sumário não pode estar vazio")
            );
        }
    }
}
