using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingArtistMvcCore.Data.ModelsData
{
    public class Enums
    {
        public enum Roles
        {
            Client,
            AnArtisi,
            Admin
        }
        public enum ArtistType
        {
            Singer,
            Keyboard,
            DJ
        }
        public enum EventType
        {
             AbBigEvent,
             MediumEvent,
             ASmallEvent

        }
    }
}
