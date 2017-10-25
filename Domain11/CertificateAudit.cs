using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Aersysm.Domain
{
    [DataContract]
    public partial  class CertificateAudit
    {
        [DataMember]
        public String AuditID { get; set; }

        [DataMember]
        public String ReguserID { get; set; }

        [DataMember]
        public String CertificateID { get; set; }

        [DataMember]
        public int  CertificateType { get; set; }
       
        [DataMember]
        public DateTime CertificateDate { get; set; }

        [DataMember]
        public String CertificatePhoto { get; set; }

        [DataMember]
        public String CertificatePhotos { get; set; }

        [DataMember]
        public DateTime SubmitDate { get; set; }

        [DataMember]
        public String AuditorID { get; set; }

        [DataMember]
        public DateTime  AuditDate { get; set; }

        [DataMember]
        public int AuditStatus { get; set; }

        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public String CertificateName { get; set; }
    }
}
