using System;

namespace ET.Numeric
{
    public static class NumericHelper
    {
        public static async ETTask<int> RequestAddAttribute(Scene zoneScene,int numericType)
        {
            M2C_AddAttributePoint m2CAddAttributePoint = null;
            try
            {
                m2CAddAttributePoint = (M2C_AddAttributePoint)await zoneScene.GetComponent<SessionComponent>().Session
                        .Call(new C2M_AddAttributePoint() { NumericType = numericType });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (m2CAddAttributePoint.Error != ErrorCode.ERR_Success)
            {
                Log.Error(m2CAddAttributePoint.Error.ToString());
                return m2CAddAttributePoint.Error; 
            }
            
            return ErrorCode.ERR_Success;
        }
    }
}