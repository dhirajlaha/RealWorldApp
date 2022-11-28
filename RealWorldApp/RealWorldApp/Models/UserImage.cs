using System;
using System.Collections.Generic;
using System.Text;

namespace RealWorldApp.Models
{
    public class UserImage
    {
        public string imageUrl { get; set; }
        public string fullImageUrl => $"https://cvehicleapp.azurewebsites.net/{imageUrl}";
    }
}
