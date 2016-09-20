---
title: "Agent and Jobs (DW)---a Technical Reference Guide for Designing Missioin-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 3e7ab1aa-6732-4409-86cc-6f5e9cff6d5a
caps.latest.revision: 4
manager: jeffreyg
---
# Agent and Jobs (DW)---a Technical Reference Guide for Designing Missioin-Critical DW Solutions
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
    <para>Enterprise customers require automation and scheduling of scripts or tasks. Both of these can be accomplished by the Microsoft SQL Server Agent and by jobs which can execute tasks on a scheduled basis, via a trigger event or on demand. Typical requirements for the creation of jobs are time-based automation, stepping mechanisms, and job status reporting.</para>
    <para>Common scheduled jobs that run through the SQL Server Agent service include database backups, database maintenance (such as index rebuilds or statistics updates), batch operations, and the execution of SQL Server Integration Services packages. Assuming that appropriate permissions are provided, the jobs allow for a variety of task types, ranging from the execution of Transact-SQL commands to the execution of external operating system calls.</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>The following section describes some best practices and potential pitfalls, and provides references for additional information. (Note that the full URLs for the hyperlinked text are provided in the Appendix at the end of this document.)</para>
      <list class="bullet">
        <listItem>
          <para>All jobs are stored in the MSDB database. Today, with SQL Server 2008 and R2, if you move a user, database jobs associated with that database are not transferred over to another server. They would need to manually be scripted out and applied (or backup/restore of MSDB) onto the other server. Many users will script the jobs and apply to both servers (with the jobs on the secondary in an inactive state).</para>
        </listItem>
        <listItem>
          <para>The SQL Server Agent subsystem has a number of worker threads allocated to it, and limited by the max_worker_threads configuration. You may need to consider increasing this number if you are running a very large number of jobs at once. Also, be aware the Service Pack 1 (SP1) for SQL Server 2008 resets the configured max_worker_threads value. See the Microsoft Support article <externalLink><linkText>FIX: Installing SQL Server 2008 Service Pack 1 may reset the "max_worker_threads" column value for a SQL Server Agent subsystem</linkText><linkUri>http://support.microsoft.com/kb/972759</linkUri></externalLink><superscript>1</superscript> for more information.</para>
        </listItem>
        <listItem>
          <para>Create SQL Server Agent proxy accounts as necessary for jobs to complete, but be wary of permissions, particularly high levels of data access, and objects outside of the SQL Server itself. For further information on the Proxy account creation, see <externalLink><linkText>Creating SQL Server Agent Proxies</linkText><linkUri>http://msdn.microsoft.com/library/ms189064.aspx</linkUri></externalLink>.<superscript>2</superscript></para>
        </listItem>
        <listItem>
          <para>SQL Server Express does not have a SQL Server Agent. There are other ways, such as using the Windows Task Scheduler, to scheduler certain maintenance.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Case Studies and Reference</title>
    <content>
      <para>Following are some helpful examples:</para>
      <list class="bullet">
        <listItem>
          <para>
            <externalLink>
              <linkText>5 Things to Consider for SQL Server Data Warehouse DBAs</linkText>
              <linkUri>http://www.sqlmag.com/blogs/sql-server-bi/tabid/1024/entryid/72327/5-Things-to-Consider-for-SQL-Server-Data-Warehouse-DBAs.aspx</linkUri>
            </externalLink>
            <superscript>3</superscript> is a great article that differentiates between OLTP and DW considerations.</para>
        </listItem>
        <listItem>
          <para>The technical note <externalLink><linkText>Scheduling Sub-Minute Log Shipping in SQL Server 2008</linkText><linkUri>http://sqlcat.com/technicalnotes/archive/2009/02/24/scheduling-sub-minute-log-shipping-in-sql-server-2008.aspx</linkUri></externalLink><superscript>4</superscript> provides an example of using jobs and job scheduling frequency. </para>
        </listItem>
        <listItem>
          <para>The article <externalLink><linkText>Restart SQL Audit Policy and Job</linkText><linkUri>http://sqlcat.com/toolbox/archive/2009/04/22/restart-sql-audit-policy-and-job.aspx</linkUri></externalLink><superscript>5</superscript> provides an example of using job scheduling to create a centralized location for audit logging.</para>
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
          <para>Many times it is multiple servers or instances that a user wants to manage from a single source. To achieve this one can implement a Master Job Server which can allocate jobs to specific target servers with the master as a centralized place to manage all the jobs.</para>
        </listItem>
        <listItem>
          <para>From our experience and based on feedback, the out-of-the-box toolset provided may not be comprehensive enough for some enterprises. There are some gaps in enterprise solution offerings for job management and scalability, and gaps in what SQL Server Agent and Master Job Servers can provide. The biggest customer requests are for more specific calendaring functions, advanced routing/conditional logic, and broader event viewing. There are some third- party vendors who try to provide a more comprehensive solution (for example, BMC Control-M or SQLSentry).</para>
        </listItem>
        <listItem>
          <para>While SQL Server Agent can run commands and even includes simple routing and logging, for more complex operations you might want to investigate SQL Server Integration Services, which has far more advanced routing, conditional branching, and logging capabilities. SQL Server Integration Services packages can be launched from within SQL Server Agent job steps.</para>
        </listItem>
        <listItem>
          <para>Ask which of the SQL Server technologies (such as database maintenance plans, log shipping, and SQL Server Replication) will be using the Agent/Jobs service to get a holistic usage scenario.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Appendix</title>
    <content>
      <para>Following are the full URLs for the hyperlinked text.</para>
      <para>
        <superscript>1</superscript> FIX: Installing SQL Server 2008 Service Pack 1 may reset the "max_worker_threads" column value for a SQL Server Agent subsystem  <externalLink><linkText>http://support.microsoft.com/kb/972759</linkText><linkUri>http://support.microsoft.com/kb/972759</linkUri></externalLink></para>
      <para>
        <superscript>2</superscript> Creating SQL Server Agent Proxies  <externalLink><linkText>http://msdn.microsoft.com/library/ms189064.aspx</linkText><linkUri>http://msdn.microsoft.com/library/ms189064.aspx</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> 5 Things to Consider for SQL Server Data Warehouse DBAs <externalLink><linkText>http://www.sqlmag.com/blogs/sql-ser</linkText><linkUri>http://www.sqlmag.com/blogs/sql-server-bi/tabid/1024/entryid/72327/5-Things-to-Consider-for-SQL-Server-Data-Warehouse-DBAs.aspx</linkUri></externalLink></para>
      <para>
        <superscript>4</superscript> Scheduling Sub-Minute Log Shipping in SQL Server 2008 <externalLink><linkText>http://sqlcat.com/technicalnotes/archive/2009/02/24/scheduling-sub-minute-log-shipping-in-sql-server-2008.aspx</linkText><linkUri>http://sqlcat.com/technicalnotes/archive/2009/02/24/scheduling-sub-minute-log-shipping-in-sql-server-2008.aspx</linkUri></externalLink></para>
      <para>
        <superscript>5</superscript> Restart SQL Audit Policy and Job  <externalLink><linkText>http://sqlcat.com/toolbox/archive/2009/04/22/restart-sql-audit-policy-and-job.aspx</linkText><linkUri>http://sqlcat.com/toolbox/archive/2009/04/22/restart-sql-audit-policy-and-job.aspx</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>