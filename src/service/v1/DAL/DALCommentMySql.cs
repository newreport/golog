﻿using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALCommentMySql : DBUtility.EFMySqlContent<Comment>, IDALComment
    {

    }
}
