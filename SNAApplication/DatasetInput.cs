﻿using Microsoft.AspNetCore.Http;

namespace SNAApplication
{
    public class DatasetInput
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Data { get; set; }

        public IFormFile File { get; set; }
    }
}
