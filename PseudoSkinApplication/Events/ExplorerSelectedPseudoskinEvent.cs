using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinApplication.Events
{
    public class ExplorerSelectedPseudoskinEvent : PubSubEvent<string>
    {
    }
    
    public class ExplorerSelectedToUpdateEvent : PubSubEvent<string>
    {
    }
    public class RegressEvent : PubSubEvent<string>
    {
    }
    public class ChartEvent : PubSubEvent
    {
    }

    public class AnisotropyChartEvent : PubSubEvent
    {
    }
    public class WellboreChartEvent : PubSubEvent
    {
    }
    public class PenRatioChartEvent : PubSubEvent
    {
    }
    public class ZmChartEvent : PubSubEvent
    {
    }
}
