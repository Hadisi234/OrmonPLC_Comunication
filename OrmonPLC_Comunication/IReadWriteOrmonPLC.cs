﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmonPLC_Comunication
{
    interface IReadWriteOrmonPLC
    {
        bool Read();
        bool Write();
         
    }
}