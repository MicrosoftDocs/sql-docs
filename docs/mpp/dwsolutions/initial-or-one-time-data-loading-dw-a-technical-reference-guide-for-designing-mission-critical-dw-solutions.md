---
title: "Initial or One-Time Data Loading (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 11772fc4-1f87-41b8-a845-ee2c741cc120
caps.latest.revision: 4
manager: jeffreyg
---
# Initial or One-Time Data Loading (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
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
    <para>Initial data loads, or large one-time loads, are extremely important, because it is with these loads that the database design (from an ETL/ELT perspective) is often verified. It is also with these loads that many issues with the entire data lineage stream appear. Initial data loads might not be characteristic of the "regular" data loads. This would apply when initial data loads need to be set up as special jobs to load historical data from archived data sources that are not in the same format and that do not contain the same business logic as regular data sources. In these scenarios, fact table data requires different ETL logic for historical data than incremental. Initial loads can run for long periods of time, and might require additional attention to prevent tempdb and log files from uncontrolled growth, and customized logic to prevent duplicates and other referential integrity issues.</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>The following resources provide additional information. (Note that the full URLs for the hyperlinked text are provided in the Appendix at the end of this document.)</para>
      <list class="bullet">
        <listItem>
          <para>The article, <externalLink><linkText>Using BULK INSERT to Load a Text File</linkText><linkUri>http://www.sqlteam.com/article/using-bulk-insert-to-load-a-text-file</linkUri></externalLink><superscript>1</superscript> provides an example of performing an initial data load.</para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>SQL Server Best Practices Article</linkText>
              <linkUri>http://technet.microsoft.com/en-us/library/cc966380.aspx</linkUri>
            </externalLink>
            <superscript>2</superscript> provides some best practices for bulk loading data.</para>
        </listItem>
        <listItem>
          <para>The technical note, <externalLink><linkText>Bulk Loading Data into a Table with Concurrent Queries</linkText><linkUri>http://sqlcat.com/technicalnotes/archive/2009/04/06/bulk-loading-data-into-a-table-with-concurrent-queries.aspx</linkUri></externalLink><superscript>3</superscript> describes load scenarios for the common data warehouse scenario in which many queries read data from a table while the table is loaded.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Case Studies and References</title>
    <content>
      <para>An example is described in the article, <externalLink><linkText>Load 1TB in Less than 1 Hour</linkText><linkUri>http://blogs.msdn.com/b/sqlcat/archive/2006/05/19/602142.aspx</linkUri></externalLink>.<superscript>4</superscript></para>
    </content>
  </section>
  <section>
    <title>Questions and Considerations</title>
    <content>
      <para>This section provides questions and issues to consider when working with your customers.</para>
      <list class="bullet">
        <listItem>
          <para>Is it possible to handle historical loads with SSIS packages used for regular (incremental) loads? This is the most ideal scenario for handling historical loads because it doesn’t require additional SSIS development and management. This can often be accomplished by adding conditional logic in SSIS packages to include Foreach Loops (see the article, <externalLink><linkText>Foreach Loop Container</linkText><linkUri>http://msdn.microsoft.com/library/ms141724.aspx</linkUri></externalLink><superscript>5</superscript>) to handle multiple source files (for example, historical files) based on some distinguishing attribute (such as client_id, and order_date).</para>
        </listItem>
        <listItem>
          <para>Plan the load sequence carefully as certain tables may need to be loaded first to help verify loads of other tables. Typically master data/dimensions need to be loaded prior to loading facts. Identify strategy for handling late arriving facts, and inferred members.</para>
        </listItem>
        <listItem>
          <para>It is usually better to perform the initial loads incrementally (for example, load a month at a time, or a certain filesize, and so on).</para>
        </listItem>
        <listItem>
          <para>Establish a detailed load strategy with expected time-frames:</para>
          <list class="bullet">
            <listItem>
              <para>How much historical data will be loaded?</para>
            </listItem>
            <listItem>
              <para>How many historical data sources are there and is the data consistent?</para>
            </listItem>
            <listItem>
              <para>Do historical dimension attributes differ?</para>
            </listItem>
            <listItem>
              <para>Will RDBMS or flat files (or both) be used?</para>
            </listItem>
            <listItem>
              <para>What is the data extract strategy from source systems?</para>
            </listItem>
            <listItem>
              <para>How will loads be verified?</para>
            </listItem>
            <listItem>
              <para>What are the clean-up steps?</para>
            </listItem>
            <listItem>
              <para>How will fragmentation be minimized?</para>
            </listItem>
            <listItem>
              <para>Should only the last instance of dimensional change be included in dimensions or all historical changes available in historical files?</para>
            </listItem>
          </list>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Appendix</title>
    <content>
      <para>Following are the full URLs for the hyperlinked text:</para>
      <para>
        <superscript>1</superscript> Using BULK INSERT to Load a Text File  <externalLink><linkText>http://</linkText><linkUri>http://www.sqlteam.com/article/using-bulk-insert-to-load-a-text-file</linkUri></externalLink><externalLink><linkText>www.sqlteam.com/article/using-bulk-insert-to-load-a-text-file</linkText><linkUri>http://www.sqlteam.com/article/using-bulk-insert-to-load-a-text-file</linkUri></externalLink></para>
      <para>
        <superscript>2</superscript> SQL Server Best Practices Article  <externalLink><linkText>http://</linkText><linkUri>http://technet.microsoft.com/library/cc966380.aspx</linkUri></externalLink><externalLink><linkText>technet.microsoft.com/library/cc966380.aspx</linkText><linkUri>http://technet.microsoft.com/library/cc966380.aspx</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> Bulk Loading Data into a Table with Concurrent Queries  <externalLink><linkText>http://</linkText><linkUri>http://sqlcat.com/technicalnotes/archive/2009/04/06/bulk-loading-data-into-a-table-with-concurrent-queries.aspx</linkUri></externalLink><externalLink><linkText>sqlcat.com/technicalnotes/archive/2009/04/06/bulk-loading-data-into-a-table-with-concurrent-queries.aspx</linkText><linkUri>http://sqlcat.com/technicalnotes/archive/2009/04/06/bulk-loading-data-into-a-table-with-concurrent-queries.aspx</linkUri></externalLink></para>
      <para>
        <superscript>4</superscript> Load 1TB in Less than 1 Hour  <externalLink><linkText>http://</linkText><linkUri>http://blogs.msdn.com/b/sqlcat/archive/2006/05/19/602142.aspx</linkUri></externalLink><externalLink><linkText>blogs.msdn.com/b/sqlcat/archive/2006/05/19/602142.aspx</linkText><linkUri>http://blogs.msdn.com/b/sqlcat/archive/2006/05/19/602142.aspx</linkUri></externalLink></para>
      <para>
        <superscript>5</superscript> Foreach Loop Container  <externalLink><linkText>http://msdn.microsoft.com/library/ms141724.aspx</linkText><linkUri>http://msdn.microsoft.com/library/ms141724.aspx</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>