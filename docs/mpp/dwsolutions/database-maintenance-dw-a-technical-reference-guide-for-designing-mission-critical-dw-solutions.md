---
title: "Database Maintenance (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: ac325128-0bb1-4f00-aff1-9621e2263743
caps.latest.revision: 5
manager: jeffreyg
---
# Database Maintenance (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
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
    <para>There are a number of administrative tasks that a database administrator (DBA) usually has to complete as part of his or her daily, weekly, monthly, or quarterly task list to keep the application and database functioning at expected levels. At the Microsoft SQL Server database level, these tasks include: </para>
    <list class="bullet">
      <listItem>
        <para>Developing a backup/restore strategy (as discussed in the Backup/Restore guide).</para>
      </listItem>
      <listItem>
        <para>Removing index fragmentation.</para>
      </listItem>
      <listItem>
        <para>Ensuring that statistics are up-to-date.</para>
      </listItem>
      <listItem>
        <para>Confirming that the database is structurally sound (by using Database Console Commands [DBCC]/database consistency checks). </para>
      </listItem>
      <listItem>
        <para>Managing the size of the data and log files.</para>
      </listItem>
    </list>
    <para>At a more detailed level, these tasks may include security patch, update, and service pack management (as discussed in the Software Maintenance guide).</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>The following resources provide reference material and additional information.</para>
      <list class="bullet">
        <listItem>
          <para>Statistics Updates are at an index or table level, not at the partition level. The trigger on auto-updatestats is (500 + 20% * table size) so as table size grows this can get called less frequently. Thinking about when to update stats (and if necessary you can do it asynchronously) and with what sampling size is important particularly on systems which are not only concerned with performance but stability. Also, sometimes stats updates can be too impactful and cause problems for the system (wait on recompile locks…). Considering the statistics sampling size is also important.</para>
        </listItem>
        <listItem>
          <para>Be sure to regularly review plans of long-running queries to ensure proper usage of indexes/stats.</para>
        </listItem>
        <listItem>
          <para>Make judicious use of indexes and covering indexes just to performance tune a handful of queries.  There is a delicate balance between tuning for queries and ensuring data loads are not impacted by numerous non-clustered indexes.</para>
        </listItem>
        <listItem>
          <para>In almost all cases for data warehouse, the clustered index column and the partitioning column should be the same.</para>
        </listItem>
        <listItem>
          <para>If statistics are regenerated, be sure to issue a DBCC freeproccache to force query plan recalculation based on new statistics. </para>
        </listItem>
        <listItem>
          <para>Develop a strategy to reorganize/rebuild indexes only when necessary (usually at a table or index level). In the article <externalLink><linkText>sys.dm_db_index-physical-stats (Transact-SQL)</linkText><linkUri>http://msdn.microsoft.com/library/ms188917.aspx</linkUri></externalLink>,<superscript>1</superscript> example D, "Using sys.dm_db_index_physical_stats in a script to rebuild or reorganize indexes," may be helpful. It is important to find a time and frequency to do the reorganization/rebuild.  There is a trade-off between the impact of maintaining/rebuilding the indexes and statistics, versus the impact of fragmentation and/or out-dated to current transaction throughput, determine whether it can be done online, and consider the impact of fill factor and of the defragmentation itself. Performing the reorganization/rebuild may have more of a negative impact than the fragmentation in the table. </para>
        </listItem>
        <listItem>
          <para>The article <externalLink><linkText>DBCC Checks and Terabyte-Scale Databases</linkText><linkUri>http://sqlcat.com/technicalnotes/archive/2009/08/13/dbcc-checks-and-terabyte-scale-databases.aspx</linkUri></externalLink><superscript>2</superscript> describes considerations for performing consistency checks on very large databases (VLDBs).   </para>
        </listItem>
        <listItem>
          <para>The white paper <externalLink><linkText>TEMPDB Capacity Planning and Concurrency Considerations for Index and Create and Rebuild</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2007/11/20/tempdb-capacity-planning-and-concurrency-considerations-for-index-create-and-rebuild.aspx</linkUri></externalLink><superscript>3</superscript> describes options for index create and rebuild operations that you can use to effectively meet the requirements of performance, concurrency, and resources. </para>
        </listItem>
        <listItem>
          <para>The ability to perform online index operations is a key reason for implementing SQL Server Enterprise or Datacenter editions, as many organizations today have limited maintenance time windows available. The article <externalLink><linkText>Online Indexing Options in SQL Server 2005</linkText><linkUri>http://technet.microsoft.com/library/cc966402.aspx</linkUri></externalLink><superscript>4</superscript> provides considerations and best practices. </para>
        </listItem>
        <listItem>
          <para>The TechNet Magazine article <externalLink><linkText>Top Tips for Effective Database Maintenance</linkText><linkUri>http://technet.microsoft.com/magazine/2008.08.database.aspx?pr=blog</linkUri></externalLink><superscript>5</superscript> provides an overview of a few database maintenance tasks </para>
        </listItem>
        <listItem>
          <para>Monitor data and log file size to understand characteristics. Goal should be to not have to shrink files and grow files only if necessary during a scheduled time, as to not impact performance. Proactively managing growth is best.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Case Studies and References</title>
    <content>
      <para>SQL Server has been deployed by customers for many Tier-1 (and lower tier) applications. </para>
      <list class="bullet">
        <listItem>
          <para>For backup architecture discussions, see the Sample Case Studies/Reference section in the Backup/Restore guide.</para>
        </listItem>
        <listItem>
          <para>Enhancements for database maintenance include the ability to perform online index rebuilds (in certain cases), rebuild in tempdb, asynchronous statistics updates, DBCC checks on file groups, backup compression, and more.</para>
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
          <para>There are many strategies to use for performance tuning a data warehouse.  Many involve non-clustered indexing, but often times simply recalculating stats can have an enormous impact.</para>
        </listItem>
        <listItem>
          <para>Consider how the maintenance may affect the performance or uptime of the data warehouse. Evaluate and/or establish maintenance windows where some tasks can be accomplished. </para>
        </listItem>
        <listItem>
          <para>Understand how data is changing in the objects and size of objects. This can be monitored through scripts and sometimes through perfmon. Understanding this can help establish expected sizes for data and log files that can be pre-allocated to certain sizes or re-allocated during a maintenance window. This can also help determine strategies for keeping indexes defragmented and statistics up-to-date.</para>
        </listItem>
        <listItem>
          <para>Understand frequency and volume of database loads.  If incremental load sizes are significant, statistics can be invalid thereby generating bad query plans.</para>
        </listItem>
        <listItem>
          <para>Consider options to reload tables rather than recreating indexes.</para>
        </listItem>
        <listItem>
          <para>Many customers have enterprise-wide backup strategies/technologies already in existence to which you need to conform.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Appendix</title>
    <content>
      <para>Following are the full URLs for the hyperlinked text.</para>
      <para>
        <superscript>1</superscript> sys.dm_db_index-physical-stats (Transact-SQL)  <externalLink><linkText>http://msdn.microsoft.com/library/ms188917.aspx</linkText><linkUri>http://msdn.microsoft.com/library/ms188917.aspx</linkUri></externalLink></para>
      <para>
        <superscript>2</superscript> DBCC Checks and Terabyte-Scale Databases  <externalLink><linkText>http://sqlcat.com/technicalnotes/archive/2009/08/13/dbcc-checks-and-terabyte-scale-databases.aspx</linkText><linkUri>http://sqlcat.com/technicalnotes/archive/2009/08/13/dbcc-checks-and-terabyte-scale-databases.aspx</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> TEMPDB Capacity Planning and Concurrency Considerations for Index and Create and Rebuild  <externalLink><linkText>http://sqlcat.com</linkText><linkUri>http://sqlcat.com/whitepapers/archive/2007/11/20/tempdb-capacity-planning-and-concurrency-considerations-for-index-create-and-rebuild.aspx</linkUri></externalLink></para>
      <para>
        <superscript>4</superscript> Online Indexing Options in SQL Server 2005  <externalLink><linkText>http://technet.microsoft.com/library/cc966402.aspx</linkText><linkUri>http://technet.microsoft.com/library/cc966402.aspx</linkUri></externalLink></para>
      <para>
        <superscript>5</superscript> Top Tips for Effective Database Maintenance  <externalLink><linkText>http://technet.microsoft.com/magazine/2008.08.database.aspx?pr=blog</linkText><linkUri>http://technet.microsoft.com/magazine/2008.08.database.aspx?pr=blog</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>