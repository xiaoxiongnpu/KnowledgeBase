﻿using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HolyNoodle.KnowledgeBase
{
    public interface IEntity
    {
        INode Node { get; set; }
    }
}
