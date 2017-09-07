﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixmensCMD.Models.dto
{
    class SmsResponseDTO
    {

        public bool success { get; set; }
        public Result result { get; set; }
    }

    public class Contact
        {
            public string id { get; set; }
            public string name { get; set; }
            public string number { get; set; }
        }

        public class Success
        {
            public string id { get; set; }
            public string device_id { get; set; }
            public string message { get; set; }
            public string status { get; set; }
            public string send_at { get; set; }
            public string queued_at { get; set; }
            public string sent_at { get; set; }
            public string delivered_at { get; set; }
            public string expires_at { get; set; }
            public string canceled_at { get; set; }
            public string failed_at { get; set; }
            public string received_at { get; set; }
            public string error { get; set; }
            public string created_at { get; set; }
            public Contact contact { get; set; }
        }

        public class Errors
        {
            public List<string> device { get; set; }
        }

        public class Fail
        {
            public string email { get; set; }
            public string password { get; set; }
            public string device { get; set; }
            public string number { get; set; }
            public string message { get; set; }
            public string send_at { get; set; }
            public string expires_at { get; set; }
            public Errors errors { get; set; }
        }

        public class Result
        {
            public List<Success> success { get; set; }
            public List<Fail> fails { get; set; }
        }

      
         
        
    }
