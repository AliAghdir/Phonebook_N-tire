﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Dto
{
    public class AddNewContactDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
    }
}
