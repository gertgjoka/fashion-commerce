//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace FashionZone.DataLayer.Model
{
    public partial class RETURN_DETAILS
    {
        #region Primitive Properties
    
        public virtual int ID
        {
            get;
            set;
        }
    
        public virtual Nullable<int> ReturnID
        {
            get { return _returnID; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_returnID != value)
                    {
                        if (RETURN != null && RETURN.ID != value)
                        {
                            RETURN = null;
                        }
                        _returnID = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<int> _returnID;
    
        public virtual Nullable<int> OrderDetailID
        {
            get { return _orderDetailID; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_orderDetailID != value)
                    {
                        if (ORDER_DETAIL != null && ORDER_DETAIL.ID != value)
                        {
                            ORDER_DETAIL = null;
                        }
                        _orderDetailID = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<int> _orderDetailID;
    
        public virtual Nullable<int> Quantity
        {
            get;
            set;
        }
    
        public virtual Nullable<decimal> Price
        {
            get;
            set;
        }
    
        public virtual int MotivationID
        {
            get { return _motivationID; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_motivationID != value)
                    {
                        if (D_RETURN_MOTIVATION != null && D_RETURN_MOTIVATION.ID != value)
                        {
                            D_RETURN_MOTIVATION = null;
                        }
                        _motivationID = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private int _motivationID;

        #endregion

        #region Navigation Properties
    
        public virtual D_RETURN_MOTIVATION D_RETURN_MOTIVATION
        {
            get { return _d_RETURN_MOTIVATION; }
            set
            {
                if (!ReferenceEquals(_d_RETURN_MOTIVATION, value))
                {
                    var previousValue = _d_RETURN_MOTIVATION;
                    _d_RETURN_MOTIVATION = value;
                    FixupD_RETURN_MOTIVATION(previousValue);
                }
            }
        }
        private D_RETURN_MOTIVATION _d_RETURN_MOTIVATION;
    
        public virtual ORDER_DETAIL ORDER_DETAIL
        {
            get { return _oRDER_DETAIL; }
            set
            {
                if (!ReferenceEquals(_oRDER_DETAIL, value))
                {
                    var previousValue = _oRDER_DETAIL;
                    _oRDER_DETAIL = value;
                    FixupORDER_DETAIL(previousValue);
                }
            }
        }
        private ORDER_DETAIL _oRDER_DETAIL;
    
        public virtual RETURN RETURN
        {
            get { return _rETURN; }
            set
            {
                if (!ReferenceEquals(_rETURN, value))
                {
                    var previousValue = _rETURN;
                    _rETURN = value;
                    FixupRETURN(previousValue);
                }
            }
        }
        private RETURN _rETURN;

        #endregion

        #region Association Fixup
    
        private bool _settingFK = false;
    
        private void FixupD_RETURN_MOTIVATION(D_RETURN_MOTIVATION previousValue)
        {
            if (D_RETURN_MOTIVATION != null)
            {
                if (MotivationID != D_RETURN_MOTIVATION.ID)
                {
                    MotivationID = D_RETURN_MOTIVATION.ID;
                }
            }
        }
    
        private void FixupORDER_DETAIL(ORDER_DETAIL previousValue)
        {
            if (previousValue != null && previousValue.RETURN_DETAILS.Contains(this))
            {
                previousValue.RETURN_DETAILS.Remove(this);
            }
    
            if (ORDER_DETAIL != null)
            {
                if (!ORDER_DETAIL.RETURN_DETAILS.Contains(this))
                {
                    ORDER_DETAIL.RETURN_DETAILS.Add(this);
                }
                if (OrderDetailID != ORDER_DETAIL.ID)
                {
                    OrderDetailID = ORDER_DETAIL.ID;
                }
            }
            else if (!_settingFK)
            {
                OrderDetailID = null;
            }
        }
    
        private void FixupRETURN(RETURN previousValue)
        {
            if (previousValue != null && previousValue.RETURN_DETAILS.Contains(this))
            {
                previousValue.RETURN_DETAILS.Remove(this);
            }
    
            if (RETURN != null)
            {
                if (!RETURN.RETURN_DETAILS.Contains(this))
                {
                    RETURN.RETURN_DETAILS.Add(this);
                }
                if (ReturnID != RETURN.ID)
                {
                    ReturnID = RETURN.ID;
                }
            }
            else if (!_settingFK)
            {
                ReturnID = null;
            }
        }

        #endregion

    }
}
