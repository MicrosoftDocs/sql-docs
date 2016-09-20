---
title: "Monitoring (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 95a785df-9617-4527-a38e-d018a4559cf3
caps.latest.revision: 5
manager: jeffreyg
---
# Monitoring (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
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
    <para>Monitoring can be viewed in a number of different ways: observing performance, auditing change, or confirming compliance of systems to data-center standards. Monitoring components in top-tier implementations is commonly an enterprise-wide initiative. These initiatives can include the deployment of particular monitoring applications or the deployment of multiple scripts that can help in monitoring the environment at finer levels of detail.</para>
    <para>Microsoft SQL Server includes several tools to help monitor an environment. Examples include the Activity Monitor, alerting mechanisms, Extended Events, Management Data Warehouse (MDW)/Data Collector, SQL Server Management Studio, SQL Profiler/SQL Trace, dynamic management views, and system views. SQL Server also integrates with Microsoft System Center Operations Manager.</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>Some pointers to monitoring tools mentioned to provide some context on what each can do:</para>
      <list class="bullet">
        <listItem>
          <para>The <externalLink><linkText>Microsoft SQL Server Management Pack</linkText><linkUri>http://www.microsoft.com/downloads/en/details.aspx?FamilyID=8c0f970e-c653-4c15-9e51-6a6cadfca363&amp;displaylang=en</linkUri></externalLink><superscript>1</superscript> for Microsoft System Center Operations Manager 2007 provides discovery and monitoring of Microsoft SQL Server 2005, SQL Server 2008, and SQL Server 2008 R2. </para>
        </listItem>
        <listItem>
          <para>For a discussion of Activity Monitor, MDW/data collector and Central Management Servers, see <externalLink><linkText>Top 10 SQL Server 2008 Features for the (DBA)</linkText><linkUri>http://sqlcat.com/top10lists/archive/2009/01/30/top-10-sql-server-2008-features-for-the-database-administrator-dba.aspx</linkUri></externalLink><externalLink><linkText>.</linkText><linkUri /></externalLink><superscript>2</superscript></para>
        </listItem>
        <listItem>
          <para>A high-level pointer to drill deeper on some of the monitoring tools functionality can be found in the article <externalLink><linkText>Monitoring (Database Engine)</linkText><linkUri>http://msdn.microsoft.com/library/bb510705.aspx</linkUri></externalLink><superscript>3</superscript> in SQL Server 2008 R2 Books Online. </para>
        </listItem>
        <listItem>
          <para>Guidance for monitoring SQL Server performance can be found in <externalLink><linkText>Troubleshooting Performance Problems in SQL Server 2008</linkText><linkUri>http://msdn.microsoft.com/library/dd672789.aspx</linkUri></externalLink>.<superscript>4</superscript></para>
        </listItem>
        <listItem>
          <para>There are a several tools that provide different insights and meet different needs within the data center. Make sure that you evaluate and choose the right tools (or set of tools) for monitoring. Some customers prefer to use third party tools because they might have prior experience and may want to standardize on tools already in use in their installation for other DBMS or tasks.  </para>
        </listItem>
        <listItem>
          <para>Tools like SQL Profiler and SQL Trace can have an impact on a high-performance environment. You may need to consider limiting monitoring, and you may need to address how to monitor from multiple facets (for example, some in the database, some via client computers, and some via "polling" solutions, like System Center Operations Manager).</para>
        </listItem>
        <listItem>
          <para>Similarly, just using dynamic management view queries without perfmon/sysmon might not provide as comprehensive a profile as you need to accurately monitor the environment.</para>
        </listItem>
        <listItem>
          <para>Try to develop a strategy for what you are going to collect, how and when it is going to be reviewed, and what you are looking for in terms of the monitoring. Ideally, the monitoring solution will not be intrusive, but will still be productive in terms of collecting what is needed to understand the environment characteristics.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Case Studies and References</title>
    <content>
      <para>The following section describes additional reference and case studies.</para>
      <para>The <externalLink><linkText>SQL CAT Community Projects &amp; Code Samples</linkText><linkUri>http://sqlcat.codeplex.com/</linkUri></externalLink><superscript>5</superscript> page on CodePlex also has some shareware for monitoring SQL Server that might work or can be modified to work, depending on the customer and the environment. The scripts and infrastructures from the SQL Server Customer Advisory Team (SQLCAT) include response time and server waits through Extended Events (a less intrusive mechanism) and a Centralized Audit Logging mechanism. The page <externalLink><linkText>Project Hosting for Open Source Software</linkText><linkUri>http://www.codeplex.com</linkUri></externalLink><superscript>6</superscript> also includes other SQL Server monitoring tools that have become popular on the site.</para>
      <para>For information about monitoring for compliance and for some general auditing tips, see the white paper <externalLink><linkText>Reaching Compliance: SQL Server 2008 Compliance Guide</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2008/11/15/reaching-compliance-sql-server-2008-compliance-guide.aspx</linkUri></externalLink>.<superscript>7</superscript> The sample scripts and tools are particularly helpful.</para>
      <para>A new/future vision for some customers might include Microsoft System Center Advisor.<superscript>8</superscript> While it may not work for some enterprise environments, it is an interesting architecture that helps provide another possible solution to customers for proactive monitoring and troubleshooting.</para>
    </content>
  </section>
  <section>
    <title>Questions and Considerations</title>
    <content>
      <para>This section provides questions and issues to consider when working with your customers.</para>
      <list class="bullet">
        <listItem>
          <para>Data warehouse monitoring is significantly different than OLTP. Also, monitoring the performance of data loads is different than monitoring queries. The primary "canaries" to watch include disk I/O and scan rates, TEMPDB operations (sorting on loads or hash-join spilling), fragmentation (creates random I/O), and CPU utilization.</para>
        </listItem>
        <listItem>
          <para>Do you have any enterprise-scoped monitoring software in-house? How does it integrate with SQL Server?</para>
        </listItem>
        <listItem>
          <para>Does your enterprise or solution have any particular service level agreements (SLAs), operational level agreements (OLAs), or other requirements for monitoring performance or compliance? If so, it is best to understand them when picking a solution for monitoring.</para>
        </listItem>
        <listItem>
          <para>Do you have any processes for collecting and reviewing data in terms of monitoring the environment(s)? </para>
        </listItem>
        <listItem>
          <para>Having the ability to monitor the application for compliance and performance is, in many cases, just as important as the application itself; therefore, it is deemed a mission-critical component of the infrastructure.</para>
        </listItem>
        <listItem>
          <para>There are a number of third-party vendors who have developed monitoring applications to work with SQL Server (for example, Idera, RedGate, Quest, and Precise).</para>
        </listItem>
        <listItem>
          <para>Some customers have found the information in the article <externalLink><linkText>The SQL Server Management System: Definition</linkText><linkUri>http://www.informit.com/guides/content.aspx?g=sqlserver&amp;seqNum=316</linkUri></externalLink><superscript>9</superscript> useful when considering monitoring solutions.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Appendix</title>
    <content>
      <para>Following are the full URLs for the hyperlinked text.</para>
      <para>
        <superscript>1</superscript> Microsoft SQL Server Management Pack  <externalLink><linkText>http://www.microsoft.com/downloads/en/details.aspx?FamilyID=8c0f970e-c653-4c15-9e51-6a6cadfca363&amp;displaylang=en</linkText><linkUri>http://www.microsoft.com/downloads/en/details.aspx?FamilyID=8c0f970e-c653-4c15-9e51-6a6cadfca363&amp;displaylang=en</linkUri></externalLink></para>
      <para>
        <superscript>2</superscript> Top 10 SQL Server 2008 Features for the (DBA)  <externalLink><linkText>http://sqlcat.com/top10lists/archive/2009/01/30/top-10-sql-server-2008-features-for-the-database-administrator-dba.aspx</linkText><linkUri>http://sqlcat.com/top10lists/archive/2009/01/30/top-10-sql-server-2008-features-for-the-database-administrator-dba.aspx</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> Monitoring (Database Engine)  <externalLink><linkText>http://msdn.microsoft.com/library/bb510705.aspx</linkText><linkUri>http://msdn.microsoft.com/library/bb510705.aspx</linkUri></externalLink></para>
      <para>
        <superscript>4</superscript> Troubleshooting Performance Problems in SQL Server 2008  <externalLink><linkText>http://msdn.microsoft.com/library/dd672789.aspx</linkText><linkUri>http://msdn.microsoft.com/en-us/library/dd672789.aspx</linkUri></externalLink></para>
      <para>
        <superscript>5</superscript> SQL CAT Community Projects &amp; Code Samples  <externalLink><linkText>http://sqlcat.codeplex.com/</linkText><linkUri>http://sqlcat.codeplex.com/</linkUri></externalLink></para>
      <para>
        <superscript>6</superscript> Project Hosting for Open Source Software  <externalLink><linkText>http://www.codeplex.com</linkText><linkUri>http://www.codeplex.com/</linkUri></externalLink></para>
      <para>
        <superscript>7</superscript> Reaching Compliance: SQL Server 2008 Compliance Guide  <externalLink><linkText>http://</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2008/11/15/reaching-compliance-sql-server-2008-compliance-guide.aspx</linkUri></externalLink><externalLink><linkText>sqlcat.com/whitepapers/archive/2008/11/15/reaching-compliance-sql-server-2008-compliance-guide.aspx</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2008/11/15/reaching-compliance-sql-server-2008-compliance-guide.aspx</linkUri></externalLink></para>
      <para>
        <superscript>8</superscript> Microsoft System Center Advisor <externalLink><linkText>http://</linkText><linkUri>http://www.microsoft.com/systemcenter/en/us/atlanta.aspx</linkUri></externalLink><externalLink><linkText>www.microsoft.com/systemcenter/en/us/atlanta.aspx</linkText><linkUri>http://www.microsoft.com/systemcenter/en/us/atlanta.aspx</linkUri></externalLink></para>
      <para>
        <superscript>9</superscript> The SQL Server Management System: Definition  <externalLink><linkText>http://www.informit.com/guides/content.aspx?g=sqlserver&amp;seqNum=316</linkText><linkUri>http://www.informit.com/guides/content.aspx?g=sqlserver&amp;seqNum=316</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>