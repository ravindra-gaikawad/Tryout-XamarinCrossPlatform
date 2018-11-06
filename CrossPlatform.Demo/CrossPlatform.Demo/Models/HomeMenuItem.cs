using System;
using System.Collections.Generic;
using System.Text;

namespace CrossPlatform.Demo.Models
{
    public enum MenuItemType
    {
        Browse,
        Samples,
        Cards,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
