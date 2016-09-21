---
title: "High Availability and Disaster Recovery (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: b77ceb67-21e4-4c06-9ccf-08a8c947c0df
caps.latest.revision: 3
manager: jeffreyg
---
# High Availability and Disaster Recovery (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
<?xml version="1.0" encoding="utf-8"?>
<developerConceptualDocument xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://ddue.schemas.microsoft.com/authoring/2003/5 http://clixdevr3.blob.core.windows.net/ddueschema/developer.xsd">
  <introduction>
    <table xmlns:caps="http://schemas.microsoft.com/build/caps/2013/11">
      <tbody>
        <tr>
          <TD>
            <para>
              <embeddedLabel>Want more guides like this one?</embeddedLabel> Go to <externalLink><linkText>Technical Reference Guides for Designing Mission-Critical Solutions</linkText><linkUri>http://technet.microsoft.com/en-us/sqlserver/hh273157</linkUri></externalLink>.</para>
          </TD>
        </tr>
      </tbody>
    </table>
    <para>Microsoft SQL Server 2008 R2 (as well as SQL Server 2008 and SQL Server 2005) offers a variety of technologies for customers to pick from to meet the high availability and disaster recovery (HA/DR) requirements of their application portfolio. Customers typically adopt at least two of these technologies to accomplish their HA/DR goals; Tier-1 deployments in particular, which are often either mission or business critical, generally require more than one technology. </para>
    <para>Examples referenced in the sections that follow show that customers can achieve up to 99.99% or 99.999% availability, even when upgrading to new versions of SQL Server and/or upgrading their hardware. Furthermore, Windows security patching can be completed while meeting availability goals. </para>
    <para>Availability goals can be met with a variety of configurations. A popular architecture combines failover clustering for HA and database mirroring for DR. Additionally, log shipping can be added to achieve tertiary site DR protection. (More information about these options can be found later in this document.)</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>The following resources provide examples of customer scenarios for implementing SQL Server HA/DR, in addition to general SQL Server HA/DR reference material.</para>
      <list class="bullet">
        <listItem>
          <para>Slide 8 in the PowerPoint presentation <externalLink><linkText>Proven Customer Deployed Architectures &amp; Scenarios For SQL Server HA/DR</linkText><linkUri>http://ecn.channel9.msdn.com/o9/te/NorthAmerica/2010/pptx/DAT401.pptx</linkUri></externalLink><superscript>1</superscript> provides customer requirements and suggested HA/DR architectures.</para>
        </listItem>
        <listItem>
          <para>The white paper <externalLink><linkText>Proven SQL Server Architectures for High Availability and Disaster Recovery</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2010/06/07/proven-sql-server-architectures-for-high-availability-and-disaster-recovery.aspx</linkUri></externalLink><superscript>2</superscript> shows the details of five commonly used architectures:</para>
          <list class="bullet">
            <listItem>
              <para>Failover clustering for HA and database mirroring for DR.</para>
            </listItem>
            <listItem>
              <para>Synchronous database mirroring for HA/DR and log shipping for additional DR.</para>
            </listItem>
            <listItem>
              <para>Geo-cluster for HA/DR and log shipping for additional DR.</para>
            </listItem>
            <listItem>
              <para>Failover clustering for HA and storage area network (SAN)-based replication for DR.</para>
            </listItem>
            <listItem>
              <para>Peer-to-peer replication for HA and DR (and reporting).</para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>The white paper <externalLink><linkText>SAP with Microsoft SQL Server 2005: Best Practices for High Availability, Maximum Performance, and Scalability</linkText><linkUri>http://download.microsoft.com/download/d/9/4/d948f981-926e-40fa-a026-5bfcf076d9b9/sap_sql2005_best%20practices.doc</linkUri></externalLink><superscript>3</superscript> describes best practices that customers, system integrators, and partners can use to design and install more reliable, highly available SAP implementations using SQL Server 2005. An update to the SAP white paper is available in the PowerPoint presentation <externalLink><linkText>Microsoft SQL Server: The Platform for SAP</linkText><linkUri>http://ecn.channel9.msdn.com/o9/te/NorthAmerica/2010/pptx/DAT314.pptx</linkUri></externalLink>.<superscript>4</superscript></para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Case Studies and References</title>
    <content>
      <para>Customers have successfully configured SQL Server to achieve up to 99.999% availability. Examples of successful architectures are described in the following case studies and white papers. </para>
      <list class="bullet">
        <listItem>
          <para>The white paper <externalLink><linkText>High Availability and Disaster Recovery at ServiceU: A SQL Server 2008 Technical Case Study</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2009/08/04/high-availability-and-disaster-recovery-at-serviceu-a-sql-server-2008-technical-case-study.aspx</linkUri></externalLink><superscript>5</superscript> describes how ServiceU, a provider of online and on-demand event management software, successfully uses failover clustering, asynchronous database mirroring, and log shipping.</para>
        </listItem>
        <listItem>
          <para>The case study <externalLink><linkText>bwin: Global Online Gaming Company Deploying SQL Server 2008 to support 100 Terabytes</linkText><linkUri>http://www.microsoft.com/casestudies/Case_Study_Detail.aspx?casestudyid=4000001470</linkUri></externalLink><superscript>6</superscript> describes how bwin uses database mirroring and log shipping.</para>
        </listItem>
        <listItem>
          <para>The white paper <externalLink><linkText>Using Replication for High Availability and Disaster Recovery</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2009/09/23/using-replication-for-high-availability-and-disaster-recovery.aspx</linkUri></externalLink><superscript>7</superscript> describes how a travel enterprise uses peer-to-peer replication and log shipping.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Questions and Considerations</title>
    <content>
      <para>This section provides questions and issues to consider when working with your customers.</para>
      <list class="bullet">
        <listItem>
          <para>Understand the business motivations and regulatory requirements that are driving the customer's HA/DR requirements. Understand how your customer categorizes the workload from an HA/DR perspective. There is likely to be an alignment between the needs and categorization.</para>
        </listItem>
        <listItem>
          <para>Ask for both the recovery time objective (RTO) and the recovery point objective (RPO) for different workload categories, for both a failure within a data center (local high availability) and a total data center failure (disaster recovery). While RPO and RTO vary for different workloads because of business, cost, or technological considerations, customers may prefer a single technical solution for ease in operations. However, a single technical solution may require trade-offs that need to be discussed with customers so that their expectations are set appropriately.</para>
        </listItem>
        <listItem>
          <para>Ask if there is an organizational preference for a particular HA/DR technology. Customers may have a preference because of previous experiences, established operational procedures, or simply the desire for uniformity across databases from different vendors. Understand the motives behind a preference: A customers' preference for HA/DR may not be because of the functions and features of the HA/DR technology. For example, a customer may decide to adopt a third-party solution for DR to maintain a single operational procedure. For this reason, using HA/DR technology provided by a SAN vendor (such as EMC SRDF) is a popular approach.   </para>
        </listItem>
        <listItem>
          <para>To design and adopt an HA/DR solution it is also important to understand the implications of applying maintenance to both hardware and software (including Windows security patching). Database mirroring is often adopted to minimize the service disruption to achieve this objective.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Appendix</title>
    <content>
      <para>Following are the full URLs for the hyperlinked text:</para>
      <para>
        <superscript>1</superscript> Proven Customer Deployed Architectures &amp; Scenarios for SQL Server HA/DR  <externalLink><linkText>http://ecn.channel9.msdn.com/o9/te/NorthAmerica/2010/pptx/DAT401.pptx</linkText><linkUri>http://ecn.channel9.msdn.com/o9/te/NorthAmerica/2010/pptx/DAT401.pptx</linkUri></externalLink></para>
      <para>
        <superscript>2</superscript> Proven SQL Server Architectures for High Availability and Disaster Recovery  <externalLink><linkText>http://sqlcat.com/whitepapers/archive/2010/06/07/proven-sql-server-architectures-for-high-availability-and-disaster-recovery.aspx</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2010/06/07/proven-sql-server-architectures-for-high-availability-and-disaster-recovery.aspx</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> SAP with Microsoft SQL Server 2005: Best Practices for High Availability, Maximum Performance, and Scalability  <externalLink><linkText>http://download.microsoft.com/download/d/9/4/d948f981-926e-40fa-a026-5bfcf076d9b9/sap_sql2005_best%20practices.doc</linkText><linkUri>http://download.microsoft.com/download/d/9/4/d948f981-926e-40fa-a026-5bfcf076d9b9/sap_sql2005_best%20practices.doc</linkUri></externalLink></para>
      <para>
        <superscript>4</superscript> Microsoft SQL Server: The Platform for SAP  <externalLink><linkText>http://ecn.channel9.msdn.com/o9/te/NorthAmerica/2010/pptx/DAT314.pptx</linkText><linkUri>http://ecn.channel9.msdn.com/o9/te/NorthAmerica/2010/pptx/DAT314.pptx</linkUri></externalLink></para>
      <para>
        <superscript>5</superscript> High Availability and Disaster Recovery at ServiceU: A SQL Server 2008 Technical Case Study  <externalLink><linkText>http://sqlcat.com/whitepapers/archive/2009/08/04/high-availability-and-disaster-recovery-at-serviceu-a-sql-server-2008-technical-case-study.aspx</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2009/08/04/high-availability-and-disaster-recovery-at-serviceu-a-sql-server-2008-technical-case-study.aspx</linkUri></externalLink></para>
      <para>
        <superscript>6</superscript> bwin: Global Online Gaming Company Deploying SQL Server 2008 to support 100 Terabytes  <externalLink><linkText>http://www.microsoft.com/casestudies/Case_Study_Detail.aspx?casestudyid=4000001470</linkText><linkUri>http://www.microsoft.com/casestudies/Case_Study_Detail.aspx?casestudyid=4000001470</linkUri></externalLink></para>
      <para>
        <superscript>7</superscript> Using Replication for High Availability and Disaster Recovery<externalLink><linkText>http://sqlcat.com/whitepapers/archive/2009/09/23/using-replication-for-high-availability-and-disaster-recovery.aspx</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2009/09/23/using-replication-for-high-availability-and-disaster-recovery.aspx</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>