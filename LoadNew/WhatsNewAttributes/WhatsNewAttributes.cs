using System;
using System.Collections.Generic;
using System.Text;

namespace WhatsNewAttributes
{
    class WhatsNewAttributes
    {
    }
    #region 自定义特性

    /// <summary>
    /// 用于标记最后一次修改数据项的时间和信息。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Constructor,
        AllowMultiple = true, Inherited = false)]
    public class LastModifiedAttribute : Attribute
    {
        private readonly DateTime _dateModified;
        private readonly string _changes;
        public LastModifiedAttribute(string dateModified, string changes)
        {
            _dateModified = DateTime.Parse(dateModified);
            _changes = changes;
        }
        public DateTime DateModified => _dateModified;
        public string Changes => _changes;
        public string Issues { get; set; }
    }

    /// <summary>
    /// 用于把程序集标记为通过LastModifiedAttribute维护的文档
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class SupportsWhatsNewAttribute : Attribute
    {

    }

    #endregion
}
