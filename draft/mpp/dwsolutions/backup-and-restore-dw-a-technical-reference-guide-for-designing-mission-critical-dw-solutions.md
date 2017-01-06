---
title: "Backup and Restore (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 6b883e44-b6ec-45d9-bd3c-bb323f40ec65
caps.latest.revision: 5
manager: jeffreyg
---
# Backup and Restore (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
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
    <para>Backup and restore are the primary mechanisms that customers use for maintaining copies of their databases in case of disaster, for moving data over to another system, and for offline storage/archive of data. Backup and restore strategy discussions usually occur in the context of operations considerations for the database engine and focus on developing a strategy for reducing data loss and maintaining the desired data availability. The backup and restore strategy should conform to the business requirements for data loss (recovery point objective [RPO]) and the ability to access that data if there is a failure (recovery time objective [RTO]).</para>
    <para>Common, more advanced approaches include partial backups and piecemeal restore sequences, which allow for partial availability and more specific backup/restore scenarios. Another common practice is to use file-level "backups," which are initiated via replication at a storage-vendor level. This practice is frequently discussed within a high availability and disaster recovery (HA/DR) context, but, in many cases, the actual copies of data and log files at a storage level are used for backup and recovery/restore sequences.</para>
    <para>Although the database (DB) backup/restore mechanism can be the same, data warehouse backup/restore strategies can be significantly different from OLTP systems. Strategies differ because of the volume of data, the window of system availability to perform backups, mission criticality of the data warehouse, developing a key incremental backup strategy (to avoid having to recover 200 TB in one shot), and so on.</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>The following section provides some best practice guidance and some pitfalls to avoid. References are provided for further information.</para>
      <list class="bullet">
        <listItem>
          <para>Trap: Not having a firm understanding of business requirements/SLA to meet in terms of downtime, and not planning your backup/restore strategy around that. A backup and recovery strategy which does not meet the business requirements is a setup for perceived failure by the business in the case of disaster.</para>
        </listItem>
        <listItem>
          <para>These pointers provide best practices and recommendations on backup compression and tuning:</para>
          <list class="bullet">
            <listItem>
              <para>
                <externalLink>
                  <linkText>Tuning the Performance of Backup Compression in SQL Server 2008</linkText>
                  <linkUri>http://sqlcat.com/technicalnotes/archive/2008/04/21/tuning-the-performance-of-backup-compression-in-sql-server-2008.aspx</linkUri>
                </externalLink>
                <superscript>1</superscript>
              </para>
            </listItem>
            <listItem>
              <para>
                <externalLink>
                  <linkText>Tuning Backup Compression Part 2</linkText>
                  <linkUri>http://sqlcat.com/technicalnotes/archive/2009/02/16/tuning-backup-compression-part-2.aspx</linkUri>
                </externalLink>
                <superscript>2</superscript>
              </para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>Some other best practices/recommendations on achieving high performance backups on large scale systems:</para>
          <list class="bullet">
            <listItem>
              <para>
                <externalLink>
                  <linkText>A Technical Case Study: Fast and Reliable Backup and Restore of Multi-Terabytes Database over the Network</linkText>
                  <linkUri>http://sqlcat.com/whitepapers/archive/2009/08/13/a-technical-case-study-fast-and-reliable-backup-and-restore-of-a-vldb-over-the-network.aspx</linkUri>
                </externalLink>
                <superscript>3</superscript>
              </para>
            </listItem>
            <listItem>
              <para>
                <externalLink>
                  <linkText>Backup More Than 1GB per Second Using SQL2008 Backup Compression</linkText>
                  <linkUri>http://sqlcat.com/msdnmirror/archive/2008/03/02/backup-more-than-1gb-per-second-using-sql2008-backup-compression.aspx</linkUri>
                </externalLink>
                <superscript>4</superscript>
              </para>
            </listItem>
            <listItem>
              <para>
                <externalLink>
                  <linkText>Scheduling Sub-Minute Log Shipping in SQL Server 2008</linkText>
                  <linkUri>http://sqlcat.com/technicalnotes/archive/2009/02/24/scheduling-sub-minute-log-shipping-in-sql-server-2008.aspx</linkUri>
                </externalLink>
                <superscript>5</superscript>
              </para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>Placing backup files in a location away from the production databases is critical. Never subject the only backup to the same potential failure component as could happen to the production database (for example, backup files residing on the same disks as production database).</para>
        </listItem>
        <listItem>
          <para>While there are ways to confirm the pages being backed-up are not corrupt (checksum on backup) the best way to confirm you have a valid backup is to do an actual restore of the backup. Practicing this process and procedure can also give a better understanding of how long a recovery process may take. It isn’t just how long it takes to do the restore, but also the time it takes to get backup files moved over to the system, execute restore commands, and then actually do the restore.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Case Studies and References</title>
    <content>
      <para>The following references provide additional information.</para>
      <list class="bullet">
        <listItem>
          <para>The white paper <externalLink><linkText>Configure disaster recovery across SharePoint farms by using SQL Server log shipping</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2009/06/05/configure-disaster-recovery-across-sharepoint-farms-by-using-sql-server-log-shipping.aspx</linkUri></externalLink><superscript>6</superscript> discusses how to configure a Microsoft Office SharePoint Server 2007 system (based on SQL Server) for disaster recovery scenarios using SQL Server backup/restore functionality.</para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>A Guide for SQL Server Backup Application Vendors</linkText>
              <linkUri>http://technet.microsoft.com/library/cc966520.aspx</linkUri>
            </externalLink>
            <superscript>7</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Very Large Data Warehouses Challenge Backup</linkText>
              <linkUri>http://www.information-management.com/issues/20050601/1028753-1.html</linkUri>
            </externalLink>
            <superscript>8</superscript>: an excellent article on the pitfalls and best practices for managing DW backups and restores.</para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>How to Design a Backup Strategy in SQL Server</linkText>
              <linkUri>http://www.petri.co.il/design-a-backup-strategy-in-sql-server.htm</linkUri>
            </externalLink>
            <superscript>9</superscript>: This is a data warehouse specific guide with some good ideas on handling backups, but also managing log files, archives, and so on.</para>
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
          <para>The SQL Server backup/restore functionality needs to be included in discussions about backup storage and performance, recovery procedures, and time to recover. Many of these steps are documented in a "run book," a data center procedures guide, or an application requirements guide. For Tier-1 customers, considerations such as partial availability, size of the backup, and integration with their current backup/restore model (for example, third-party tool or disk or tape storage, and security of backups) are all critical considerations.</para>
        </listItem>
        <listItem>
          <para>Discuss the enterprise’s ability to handle data loss and potential downtime. Business users are often unprepared for either but ensuring zero downtime and zero potential data loss is usually viable for only selected workloads. Understanding the requirements can help with the modeling a high-level backup/restore strategy.</para>
        </listItem>
        <listItem>
          <para>Understand how the application code plans to call the DBMS. Will it use Static or Dynamic SQL? To what extent will it use SP? Will the answer set be very large over slow TP links to the end user? For more discussion, see orthogonal architecture on Application Data Layer.</para>
        </listItem>
        <listItem>
          <para>Understand HA/DR requirements and match them to the architectures that are discussed in HA/DR technical reference guides. The backup/restore strategy needs to be aligned with the HA/DR strategy. More detail is provided in the best practices sections in the HA/DR guides.</para>
        </listItem>
        <listItem>
          <para>Understand if projected volumes can be met simply by scaling up, or if a future desire is to open the system to large number of users, and scale-out will be necessary. If possible, design the system for scale-out.</para>
        </listItem>
        <listItem>
          <para>If an ISV application, ask how the current deployment compares with the earlier ones to get a good handle on hardware requirements and potential operational considerations.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Appendix</title>
    <content>
      <para>Following are the full URLs for the hyperlinked text.</para>
      <para>
        <superscript>1</superscript> Tuning the Performance of Backup Compression in SQL Server 2008  <externalLink><linkText>http://sqlcat.com/technicalnotes/archive/2008/04/21</linkText><linkUri>http://sqlcat.com/technicalnotes/archive/2008/04/21/tuning-the-performance-of-backup-compression-in-sql-server-2008.aspx</linkUri></externalLink></para>
      <para>
        <superscript>2</superscript> Tuning Backup Compression Part 2  <externalLink><linkText>http://sqlcat.com/technicalnotes/archive/2009/02/16/tuning-backup-compression-part-2.aspx</linkText><linkUri>http://sqlcat.com/technicalnotes/archive/2009/02/16/tuning-backup-compression-part-2.aspx</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> A Technical Case Study: Fast and Reliable Backup and Restore of Multi-Terabytes Database over the Network  <externalLink><linkText>http://sqlcat.com/whitepapers/archive/2009/08/13</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2009/08/13/a-technical-case-study-fast-and-reliable-backup-and-restore-of-a-vldb-over-the-network.aspx</linkUri></externalLink></para>
      <para>
        <superscript>4</superscript> Backup More Than 1GB per Second Using SQL2008 Backup Compression  <externalLink><linkText>http://sqlcat.com/msdnmirror/archive/2008/03/02</linkText><linkUri>http://sqlcat.com/msdnmirror/archive/2008/03/02/backup-more-than-1gb-per-second-using-sql2008-backup-compression.aspx</linkUri></externalLink></para>
      <para>
        <superscript>5</superscript> Scheduling Sub-Minute Log Shipping in SQL Server 2008  <externalLink><linkText>http://sqlcat.com/technicalnotes/archive/2009/02/24</linkText><linkUri>http://sqlcat.com/technicalnotes/archive/2009/02/24/scheduling-sub-minute-log-shipping-in-sql-server-2008.aspx</linkUri></externalLink></para>
      <para>
        <superscript>6</superscript> Configure disaster recovery across SharePoint farms by using SQL Server log shipping  <externalLink><linkText>http://sqlcat.com/whitepapers/archive/2009/06/05</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2009/06/05/configure-disaster-recovery-across-sharepoint-farms-by-using-sql-server-log-shipping.aspx</linkUri></externalLink></para>
      <para>
        <superscript>7</superscript> A Guide for SQL Server Backup Application Vendors  <externalLink><linkText>http://technet.microsoft.com/library/cc966520.aspx</linkText><linkUri>http://technet.microsoft.com/library/cc966520.aspx</linkUri></externalLink></para>
      <para>
        <superscript>8</superscript> Very Large Data Warehouses Challenge Backup  <externalLink><linkText>http://www.information-management.com/issues/20050601/1028753-1.html</linkText><linkUri>http://www.information-management.com/issues/20050601/1028753-1.html</linkUri></externalLink></para>
      <para>
        <superscript>9</superscript> How to Design a Backup Strategy in SQL Server  <externalLink><linkText>http://www.petri.co.il/design-a-backup-strategy-in-sql-server.htm</linkText><linkUri>http://www.petri.co.il/design-a-backup-strategy-in-sql-server.htm</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>