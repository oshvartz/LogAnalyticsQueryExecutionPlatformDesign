using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SandBoxRunner
{
    [DataContract]
    public class ScheduledAlertRuleConditionCheckActorModel 
    {
        public ScheduledAlertRuleConditionCheckActorModel()
        {
            Version = 1;
        }

        [DataMember]
        public string AlertRuleRunId { get; set; }

        [DataMember]
        public int Version { get; set; }

        [DataMember]
        public string RuleId { get; set; }

        [DataMember]
        public string DisplayName { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public Severity Severity { get; set; }

        [DataMember]
        public string Query { get; set; }

        [DataMember]
        public string Permissions { get; set; }

        [DataMember]
        public DateTime QueryStartTimeUtc { get; set; }

        [DataMember]
        public DateTime QueryEndTimeUtc { get; set; }

        
        [DataMember]
        public int TriggerThreshold { get; set; }

        [DataMember]
        public Guid WorkspaceId { get; set; }

        [DataMember]
        public string WorkspaceName { get; set; }

        [DataMember]
        public string WorkspaceRegion { get; set; }

        [DataMember]
        public Guid WorkspaceSubscriptionId { get; set; }

        [DataMember]
        public string WorkspaceTenantId { get; set; }

        [DataMember]
        public string WorkspaceResourceGroup { get; set; }

        
        [DataMember]
        public string[] Tactics { get; set; }

        [DataMember]
        public Dictionary<string, string> CustomFields { get; set; }

        
        [DataMember]
        public string AlertRuleTemplateId { get; set; }

    }
}
