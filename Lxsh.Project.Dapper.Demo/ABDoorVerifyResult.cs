
using System.ComponentModel.DataAnnotations.Schema;

namespace Lxsh.Project.Dapper.Demo
{
    /// <summary>
    /// 
    /// </summary>
    [Table("ABDoorVerifyResult")]
    public class ABDoorVerifyResult
    {
        /// <summary>
        /// 
        /// </summary>
        public ABDoorVerifyResult()
        {
        }

        private System.Guid _SeqNo;
        /// <summary>
        /// 
        /// </summary>
        public System.Guid SeqNo { get { return this._SeqNo; } set { this._SeqNo = value; } }

        private System.Guid _InOutSeqNo;
        /// <summary>
        /// 
        /// </summary>
        public System.Guid InOutSeqNo { get { return this._InOutSeqNo; } set { this._InOutSeqNo = value; } }

        private System.String _Portrait;
        /// <summary>
        /// 
        /// </summary>
        public System.String Portrait { get { return this._Portrait; } set { this._Portrait = value; } }

        private System.Int32? _VisitorVerifyResult;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? VisitorVerifyResult { get { return this._VisitorVerifyResult; } set { this._VisitorVerifyResult = value; } }

        private System.String _VisitorVerifyMode;
        /// <summary>
        /// 
        /// </summary>
        public System.String VisitorVerifyMode { get { return this._VisitorVerifyMode; } set { this._VisitorVerifyMode = value; } }

        private System.Int32? _InOutType;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? InOutType { get { return this._InOutType; } set { this._InOutType = value; } }

        private System.DateTime? _CheckDateTime;
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? CheckDateTime { get { return this._CheckDateTime; } set { this._CheckDateTime = value; } }

        private System.Int32? _PlatType;
        /// <summary>
        /// 
        /// </summary>
        public System.Int32? PlatType { get { return this._PlatType; } set { this._PlatType = value; } }

        private System.String _CheckUeser;
        /// <summary>
        /// 
        /// </summary>
        public System.String CheckUeser { get { return this._CheckUeser; } set { this._CheckUeser = value; } }
    }
}