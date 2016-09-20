---
title: "Database Mirroring (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 13db84df-43aa-453f-b580-3b96b6377e02
caps.latest.revision: 4
manager: jeffreyg
---
# Database Mirroring (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
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
    <para>Database mirroring maintains two copies of a single database that must reside on different server instances of Microsoft SQL Server Database Engine. Typically, these server instances reside on computers in different locations. Database mirroring is not widely used for data warehousing (DW) given its overhead. Instead, most clients will resort to SAN-based replication, where copying of the database files is handled on a scheduled basis "behind the scenes," rather than as a managed operation under SQL Server.</para>
    <para>Another popular method of mirroring is to split DW loads into two streams and load both databases concurrently. After loads are finished, a manual reconciliation process must be designed to ensure that both databases are synchronized.</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>The following resources provide useful pointers for architecting and implementing database mirroring based solutions.</para>
      <list class="bullet">
        <listItem>
          <para>For database mirroring performance considerations, failover characteristics, and best practices, see <externalLink><linkText>Database Mirroring Best Practices and Performance Considerations</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2007/11/19/database-mirroring-best-practices-and-performance-considerations.aspx</linkUri></externalLink><superscript>1</superscript> The article was written for SQL Server 2005; however, most considerations apply to SQL Server2008 and SQL Server 2008 R2 also.</para>
        </listItem>
        <listItem>
          <para>The article <externalLink><linkText>Implementing Application Failover with Database Mirroring</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2007/12/16/implementing-application-failover-with-database-mirroring.aspx</linkUri></externalLink><superscript>2</superscript> illustrates how to set up connection string to work with database mirroring.</para>
        </listItem>
        <listItem>
          <para>SQL Server 2008 introduced log stream compression with database mirroring, resulting in significant performance gain. For more information, see the following articles:</para>
          <list class="bullet">
            <listItem>
              <para>
                <externalLink>
                  <linkText>Database Mirroring Log Compression in SQL Server 2008 Improves Throughput</linkText>
                  <linkUri>http://sqlcat.com/technicalnotes/archive/2007/09/17/database-mirroring-log-compression-in-sql-server-2008-improves-throughput.aspx</linkUri>
                </externalLink>
                <superscript>3</superscript>
              </para>
            </listItem>
            <listItem>
              <para>
                <externalLink>
                  <linkText>Asynchronous Database Mirroring with Log Compression in SQL Server 2008</linkText>
                  <linkUri>http://sqlcat.com/technicalnotes/archive/2007/12/17/asynchronous-database-mirroring-with-log-compression-in-sql-server-2008.aspx</linkUri>
                </externalLink>
                <superscript>4</superscript>
              </para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>If you have hundreds of databases in an instance that you want to mirror, see <externalLink><linkText>Mirroring a Large Number of Databases in a Single SQL Server Instance</linkText><linkUri>http://sqlcat.com/technicalnotes/archive/2010/02/10/mirroring-a-large-number-of-databases-in-a-single-sql-server-instance.aspx</linkUri></externalLink><superscript>5</superscript> for information about memory settings, thread settings, and other considerations.<externalLink><linkText /><linkUri /></externalLink></para>
        </listItem>
        <listItem>
          <para>Techniques in Enabling Read Committed Snapshot Isolation (RCSI) are discussed in <externalLink><linkText>How to Enable RCSI for a Database with Database Mirroring</linkText><linkUri>http://sqlcat.com/msdnmirror/archive/2010/03/16/how-to-enable-rcsi-for-a-database-with-database-mirroring.aspx</linkUri></externalLink>.<superscript>6</superscript></para>
        </listItem>
        <listItem>
          <para>Log shipping is often used along with database mirroring, either as the primary solution or for additional redundancy. For more information, view <externalLink><linkText>Database Mirroring and Log Shipping Working Together</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2008/01/21/database-mirroring-and-log-shipping-working-together.aspx</linkUri></externalLink>.<superscript>7</superscript></para>
        </listItem>
        <listItem>
          <para>Database mirroring does not support distributed transactions or Microsoft distributed transaction coordinator (MSDTC) transactions. For more information, see the following articles:</para>
          <list class="bullet">
            <listItem>
              <para>
                <externalLink>
                  <linkText>Using database mirroring for cross-database transactions or distributed transactions is not supported in SQL Server</linkText>
                  <linkUri>http://support.microsoft.com/kb/926150</linkUri>
                </externalLink>
                <superscript>8</superscript>
              </para>
            </listItem>
            <listItem>
              <para>
                <externalLink>
                  <linkText>Database Mirroring and Cross-Database Transactions</linkText>
                  <linkUri>http://msdn.microsoft.com/library/ms366279(SQL.90).aspx</linkUri>
                </externalLink>
                <superscript>9</superscript>
              </para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>The protection is at the user database level. Must create scripts and processes for copying logins, jobs, and so on, from principal to mirror.</para>
        </listItem>
        <listItem>
          <para>Only one secondary. Augment with log shipping if multiple secondaries are needed.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Case Studies and References</title>
    <content>
      <para>SQL Server database mirroring has successfully been deployed to achieve 99.99% and 99.999% availability by several customers. Examples are outlined in the following: </para>
      <list class="bullet">
        <listItem>
          <para>
            <externalLink>
              <linkText>High Availability and Disaster Recovery at ServiceU: A SQL Server 2008 Technical Case Study</linkText>
              <linkUri>http://sqlcat.com/whitepapers/archive/2009/08/04/high-availability-and-disaster-recovery-at-serviceu-a-sql-server-2008-technical-case-study.aspx</linkUri>
            </externalLink>
            <superscript>10</superscript> provides a detailed description of the end-to-end HA and DR solution at ServiceU. ServiceU deploys failover clustering for local HA and database mirroring for DR.</para>
        </listItem>
        <listItem>
          <para>Bwin uses synchronous database mirroring across data centers, and augments the solution with log shipping: <externalLink><linkText>Failure Is Not an Option: Zero Data Loss and High Availability</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2010/11/03/failure-is-not-an-option-zero-data-loss-and-high-availability.aspx</linkUri></externalLink>.<superscript>11</superscript></para>
        </listItem>
        <listItem>
          <para>The article <externalLink><linkText>High Availability and Disaster Recovery for Microsoft’s SAP Data Tier: A SQL Server 2008 Technical Case Study</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2010/10/29/high-availability-and-disaster-recovery-for-microsoft-s-sap-data-tier-a-sql-server-2008-technical-case-study.aspx</linkUri></externalLink><superscript>12</superscript> discusses how the Microsoft IT SAP deployment uses synchronous database mirroring within a data center, and log shipping to a remote data center.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Questions and Considerations</title>
    <content>
      <para>This section provides questions and issues to consider when working with your customer.</para>
      <list class="bullet">
        <listItem>
          <para>Understand how HA/DR requirements are prioritized for the application. In particular, consider the following questions:</para>
          <list class="bullet">
            <listItem>
              <para>What is the recovery point objective (RPO)? A zero data loss solution implies deploying synchronous database mirroring.</para>
            </listItem>
            <listItem>
              <para>What is the recovery time objective (RTO)? A low RTO value requires much more preparation because most of the recovery procedures have to be scripted and rigorously tested. Examples of required scripts include coordinating the failover of all the databases, making them consistent from the application perspective, and if necessary, redirecting traffic to the new application servers. </para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>Does the application use multiple databases and require that all of them be co-resident on a server? Distributed transactions are not supported regardless of their co-residency requirement. You must ensure that all databases are consistent from an application perspective prior to making them available to an application. This impacts the RTO and requires more planning and testing.</para>
        </listItem>
        <listItem>
          <para>What is the log generation rate? This has an influence on the latency of the log transmission, and for synchronous mirroring, might impact the performance of the primary database.</para>
        </listItem>
        <listItem>
          <para>Consider the network bandwidth and network latency between servers? Database mirroring performance can be impacted by network caused latencies.</para>
        </listItem>
        <listItem>
          <para>Is automatic failover needed? Automatic failover is supported only in database mirroring sessions running with a witness in high-safety mode.</para>
        </listItem>
        <listItem>
          <para>Do you need multiple secondaries in SQL Server 2008? Consider log shipping along with database mirroring.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Appendix</title>
    <content>
      <para>Following are the full URLs for the hyperlinked text.</para>
      <para>
        <superscript>1</superscript> Database Mirroring Best Practices and Performance Considerations  <externalLink><linkText>http://sqlcat.com/whitepapers/archive/2007/11/19/database-mirroring-best-practices-and-performance-considerations.aspx</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2007/11/19/database-mirroring-best-practices-and-performance-considerations.aspx</linkUri></externalLink></para>
      <para>
        <superscript>2</superscript> Implementing Application Failover with Database Mirroring  <externalLink><linkText>http://sqlcat.com/whitepapers/archive/2007/12/16/implementing-application-failover-with-database-mirroring.aspx</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2007/12/16/implementing-application-failover-with-database-mirroring.aspx</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> Database Mirroring Log Compression in SQL Server 2008 Improves Throughput  <externalLink><linkText>http://sqlcat.com/technicalnotes/archive/2007/09/17/database-mirroring-log-compression-in-sql-server-2008-improves-throughput.aspx</linkText><linkUri>http://sqlcat.com/technicalnotes/archive/2007/09/17/database-mirroring-log-compression-in-sql-server-2008-improves-throughput.aspx</linkUri></externalLink></para>
      <para>
        <superscript>4</superscript> Asynchronous Database Mirroring with Log Compression in SQL Server 2008  <externalLink><linkText>http://sqlcat.com/technicalnotes/archive/2007/12/17/asynchronous-database-mirroring-with-log-compression-in-sql-server-2008.aspx</linkText><linkUri>http://sqlcat.com/technicalnotes/archive/2007/12/17/asynchronous-database-mirroring-with-log-compression-in-sql-server-2008.aspx</linkUri></externalLink></para>
      <para>
        <superscript>5</superscript> Mirroring a Large Number of Databases in a Single SQL Server Instance  <externalLink><linkText>http://sqlcat.com/technicalnotes/archive/2010/02/10/mirroring-a-large-number-of-databases-in-a-single-sql-server-instance.aspx</linkText><linkUri>http://sqlcat.com/technicalnotes/archive/2010/02/10/mirroring-a-large-number-of-databases-in-a-single-sql-server-instance.aspx</linkUri></externalLink></para>
      <para>
        <superscript>6</superscript> How to Enable RCSI for a Database with Database Mirroring  <externalLink><linkText>http://sqlcat.com/msdnmirror/archive/2010/03/16/how-to-enable-rcsi-for-a-database-with-database-mirroring.aspx</linkText><linkUri>http://sqlcat.com/msdnmirror/archive/2010/03/16/how-to-enable-rcsi-for-a-database-with-database-mirroring.aspx</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>