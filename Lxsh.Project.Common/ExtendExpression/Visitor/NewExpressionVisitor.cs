﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Common.ExtendExpression.Visitor
{
    public class NewExpressionVisitor : ExpressionVisitor
    {
        public ParameterExpression _NewParameter { get; private set; }
        public NewExpressionVisitor(ParameterExpression param)
        {
            this._NewParameter = param;
        }
        public Expression Replace(Expression exp)
        {
            return this.Visit(exp);
        }
        protected override Expression VisitParameter(ParameterExpression node)
        {
            return this._NewParameter;
        }
    }
}
