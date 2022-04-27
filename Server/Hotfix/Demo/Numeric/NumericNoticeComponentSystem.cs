using ET.EventType;

namespace ET
{
    [FriendClass(typeof(NumericNoticeComponent))]
    public static class NumericNoticeComponentSystem
    {
        public static void NoticeImmediately(this NumericNoticeComponent self,NumbericChange args)
        {
            Unit unit = self.GetParent<Unit>();
            self.NoticeUnitNumericMessage.UnitId = unit.Id;
            self.NoticeUnitNumericMessage.NumericType = args.NumericType;
            self.NoticeUnitNumericMessage.NewValue = args.New;
            MessageHelper.SendToClient(unit, self.NoticeUnitNumericMessage);
        }
    }
}