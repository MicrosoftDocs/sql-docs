---
title: "Software Maintenance (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 14675752-b99d-47bb-9c36-42469889986b
caps.latest.revision: 3
manager: jeffreyg
---
# Software Maintenance (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
<?xml version="1.0" encoding="utf-8"?>
<developerConceptualDocument xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://ddue.schemas.microsoft.com/authoring/2003/5 http://clixdevr3.blob.core.windows.net/ddueschema/developer.xsd">
  <introduction>
    <table xmlns:caps="http://schemas.microsoft.com/build/caps/2013/11">
      <tbody>
        <tr>
          <TD>
            <para>
              <embeddedLabel>Want more guides like this one?</embeddedLabel> Go to <externalLink><linkText>Technical Reference Guides for Designing Mission-Critical Solutions</linkText><linkUri>http://technet.microsoft.com/sqlserver/hh273157</linkUri></externalLink>.</para>
          </TD>
        </tr>
      </tbody>
    </table>
    <para>Updates to software—whether for supportability, bug fixes, or for issue (or security issue) prevention—are a common characteristic that needs to be considered as part of every solution. For Microsoft SQL Server, cumulative updates (CUs), service packs (SPs), and security patches are three of the most common forms of software maintenance.</para>
    <para>Software maintenance can potentially lead to downtime and have an impact on the application/solution in terms of its use and the changes it may make on the system. Therefore, processes and procedures need to be implemented with these considerations in mind.</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>This section provides some best practice guidance and resources for more information.</para>
      <list class="bullet">
        <listItem>
          <para>With SQL Server 2008 Service Pack 1 (SP1), we allow for slipstreaming of the SP and release bits. For new deployments, or new builds which will be deployed, this provides an easier way of getting the benefits of Service Pack’s with a single install.</para>
        </listItem>
        <listItem>
          <para>Formalize a testing and release management process for the software updates. Process other changes to the solution (for example, application changes) into the overall scope and implementation of the database software changes. </para>
        </listItem>
        <listItem>
          <para>Establish or reference the run book or set of guidelines and procedures for software updates and maintenance in the datacenter.</para>
        </listItem>
        <listItem>
          <para>For background on the SQL Server 2008 servicing installation requirements and process review, see <externalLink><linkText>Overview of SQL Server Servicing Installation</linkText><linkUri>http://msdn.microsoft.com/library/dd638062(SQL.100).aspx</linkUri></externalLink>.<superscript>1</superscript></para>
        </listItem>
        <listItem>
          <para>There are potential ways to minimize the downtime for software maintenance and upgrade. A few successful implementations include ServiceU (Planned Downtime Scenario using Failover Clustering and DB Mirroring).</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Case Studies and References</title>
    <content>
      <para>The following case studies can be used for reference.</para>
      <list class="bullet">
        <listItem>
          <para>There are several ways to minimize software maintenance and upgrade downtime. An example of a successful implementation is ServiceU, which uses a planned downtime scenario using failover clustering and database mirroring. See <externalLink><linkText>High Availability and Disaster Recovery at ServiceU: A SQL Server 2008 Technical Case Study</linkText><linkUri>http://msdn.microsoft.com/library/ee355221.aspx</linkUri></externalLink>.<superscript>2</superscript> In the same location, view the subsections: Planned Downtime Scenarios, Patches and Cumulative Updates, and Database and Application Changes.</para>
        </listItem>
        <listItem>
          <para>For an example of how downtime was minimized during planned failovers, see the article <externalLink><linkText>Minimize downtime with DB Mirroring</linkText><linkUri>http://blogs.msdn.com/b/sqlcat/archive/2009/02/09/minimize-downtime-with-db-mirroring.aspx</linkUri></externalLink>.<superscript>3</superscript></para>
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
          <para>Consider the downtime (if any) for the data warehouse and make sure that all business users are notified appropriately. </para>
        </listItem>
        <listItem>
          <para>Consider having a "test and dev" environment onto which software updates are applied first, before attempting them in production. For large data warehouses, this can be a significant.</para>
        </listItem>
        <listItem>
          <para>If necessary, ensure database backups are up-to-date.</para>
        </listItem>
        <listItem>
          <para>Consider risk and impact of downtime in applying the software update. Potential for rollback procedure. </para>
        </listItem>
        <listItem>
          <para>Are there processes or procedures currently in place for software maintenance in the environment? For example, is there a monthly or quarterly maintenance window?</para>
        </listItem>
        <listItem>
          <para>For guidelines on product support, see the <externalLink><linkText>Microsoft Support Lifecycle</linkText><linkUri>http://support.microsoft.com/?LN=en-us&amp;scid=gp%3B%5Bln%5D%3Blifecycle&amp;x=7&amp;y=17</linkUri></externalLink><superscript>4</superscript> website. See the "Security Update Policy" section for the update release policy.</para>
        </listItem>
        <listItem>
          <para>For information about the security notification system, see <externalLink><linkText>Microsoft Technical Security Notification</linkText><linkUri>http://technet.microsoft.com/security/dd252948.aspx</linkUri></externalLink>.<superscript>5</superscript></para>
        </listItem>
        <listItem>
          <para>There are many layers to software maintenance for a system to consider. In the case of SQL Server we usually also need to take into account the underlying Windows OS, and the maintenance of that as well.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Appendix</title>
    <content>
      <para>Following are the full URLs for the hyperlinked text.</para>
      <para>
        <superscript>1</superscript> Overview of SQL Server Servicing Installation  <externalLink><linkText>http://msdn.microsoft.com/library/dd638062(SQL.100).aspx</linkText><linkUri>http://msdn.microsoft.com/library/dd638062(SQL.100).aspx</linkUri></externalLink></para>
      <para>
        <superscript>2</superscript> High Availability and Disaster Recovery at ServiceU: A SQL Server 2008 Technical Case Study  <externalLink><linkText>http://msdn.microsoft.com/library/ee355221.aspx</linkText><linkUri>http://msdn.microsoft.com/library/ee355221.aspx</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> Minimize downtime with DB Mirroring  <externalLink><linkText>http://blogs.msdn.com/b/sqlcat/archive/2009/02/09/minimize-downtime-with-db-mirroring.aspx</linkText><linkUri>http://blogs.msdn.com/b/sqlcat/archive/2009/02/09/minimize-downtime-with-db-mirroring.aspx</linkUri></externalLink></para>
      <para>
        <superscript>4</superscript> Microsoft Support Lifecycle  <externalLink><linkText>http://support.microsoft.com/?LN=en-us&amp;scid=gp%3B%5Bln%5D%3Blifecycle&amp;x=7&amp;y=17#tab0</linkText><linkUri>http://support.microsoft.com/?LN=en-us&amp;scid=gp%3B%5Bln%5D%3Blifecycle&amp;x=7&amp;y=17</linkUri></externalLink></para>
      <para>
        <superscript>5</superscript> Microsoft Technical Security Notification  <externalLink><linkText>http://technet.microsoft.com/security/dd252948.aspx</linkText><linkUri>http://technet.microsoft.com/security/dd252948.aspx</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>