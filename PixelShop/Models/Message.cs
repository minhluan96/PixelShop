using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PixelShop.Models
{
    public class Message
    {
        private string cssClassName;
        private string title;
        private string message;

        public string CssClassName
        {
            get
            {
                return cssClassName;
            }

            set
            {
                cssClassName = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }

        public string MessageAlert
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
            }
        }
    }
}