﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelMVC.Models
{
    public class RoomModel
    {
        public int Id { get; set; }
        public string RoomNo { get; set; }
        public string RoomDescription { get; set; }
        public decimal DailyPrice { get; set; }
        public int Beds { get; set; }

    }
}