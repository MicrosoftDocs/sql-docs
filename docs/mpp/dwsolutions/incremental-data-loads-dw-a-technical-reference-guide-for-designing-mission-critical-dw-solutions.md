---
title: "Incremental Data Loads (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 00894287-6e6e-4d23-aadc-5f6db70bdee5
caps.latest.revision: 4
manager: jeffreyg
---
# Incremental Data Loads (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
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
    <para>Incremental data loads are typically the everyday "regular" loads into the data warehouse. These "jobs" are scheduled on recurring basis. The load frequency can vary between real-time to daily, weekly, monthly, and so on. Often times, incremental load strategies have matured slowly over time where the early load jobs were relatively unplanned/unsophisticated ("just get the data in") to more mature environments where loads are carefully planned (error-recovery, minimize data fragmentation, data validation, and so on).</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>The following resources provide reference material and additional information.</para>
      <list class="bullet">
        <listItem>
          <para>When loading incremental data into a populated, clustered, partitioned table, you will incur full logging if you do not use Trace Flag 610. If the order if the source data is not the same as the clustered or partition key, you will incur full logging due to page split even if you use TF610.</para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>The Data Loading Performance Guide</linkText>
              <linkUri>http://msdn.microsoft.com/library/dd425070.aspx</linkUri>
            </externalLink>
            <superscript>1</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>SQL Server Best Practices Article</linkText>
              <linkUri>http://technet.microsoft.com/library/cc966380.aspx</linkUri>
            </externalLink>
            <superscript>2</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Bulk Loading into a Table with Concurrent Queries</linkText>
              <linkUri>http://sqlcat.com/technicalnotes/archive/2009/04/06/bulk-loading-data-into-a-table-with-concurrent-queries.aspx</linkUri>
            </externalLink>
            <superscript>3</superscript>
          </para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Case Studies and References</title>
    <content>
      <para>
        <externalLink>
          <linkText>Project REAL: Business Intelligence ETL Design Practices</linkText>
          <linkUri>http://www.microsoft.com/downloads/en/details.aspx?FamilyID=705B03F3-1BBF-417F-9E63-92A00A4744E6</linkUri>
        </externalLink>
        <superscript>4</superscript>
      </para>
    </content>
  </section>
  <section>
    <title>Questions and Considerations</title>
    <content>
      <para>This section provides questions and issues to consider when working with your customers.</para>
      <list class="bullet">
        <listItem>
          <para>Plan the Load Sequence carefully as certain tables may need to be loaded first to help verify loads of other tables. This is typically the case with loading dimensions first to generate surrogate keys, followed by fact loads where dimension surrogate keys are looked up and inserted in facts. Consider strategy for addressing dimension inferred members which will be applied to handle generating dimension members during fact load where dimension lookups yield no match.</para>
        </listItem>
        <listItem>
          <para>Consider using incremental loading strategy for historical loads, when format and grain of historical files allow this, rather than loading all historical data at once. This will also help with reducing the number of SSIS packages by combining historical and incremental load routines in a single package. Additionally, partitioning of historical loads should have a positive effect on preventing tempdb and log file sizes from abnormal expansion.</para>
        </listItem>
        <listItem>
          <para>Consider incorporating Lookup and Conditional Split SSIS components to identify and manage changes in data being loaded to implement appropriate increment loading strategy (see <externalLink><linkText>SSIS Design Pattern – Incremental Loads</linkText><linkUri>http://sqlblog.com/blogs/andy_leonard/archive/2007/07/09/ssis-design-pattern-incremental-loads.aspx</linkUri></externalLink><superscript>5</superscript>).</para>
        </listItem>
        <listItem>
          <para> Take advantage of Change Data Capture in SQL 2008 Enterprise for incremental data loads. In SQL Server, change data capture offers an effective solution to the challenge of efficiently performing incremental loads from source tables to data marts and data warehouses. See <externalLink><linkText>Improving Incremental Loads with Change Data Capture</linkText><linkUri>http://msdn.microsoft.com/library/bb895315.aspx</linkUri></externalLink><superscript>6</superscript> and <externalLink><linkText>How To Process Change Data Capture (CDC) in SQL Server Integration Services SSIS 2008</linkText><linkUri>http://www.mssqltips.com/tip.asp?tip=1755</linkUri></externalLink>.<superscript>7</superscript></para>
        </listItem>
        <listItem>
          <para>Consider CDC solutions for other platform, such as Oracle CDC for SSIS (see <externalLink><linkText>Attunity Oracle-CDC for SSIS</linkText><linkUri>http://www.attunity.com/oracle_cdc_for_ssis</linkUri></externalLink><superscript>8</superscript>). </para>
        </listItem>
        <listItem>
          <para>If CHECKSUM is considered as an option to assist in determining changes for incremental loads, make sure that columns used for computations do provide for true uniqueness of records. See <externalLink><linkText>SSIS – Using a checksum to determine if a row has changed</linkText><linkUri>http://www.ssistalk.com/2007/03/09/ssis-using-a-checksum-to-determine-if-a-row-has-changed/</linkUri></externalLink>.<superscript>9</superscript></para>
        </listItem>
        <listItem>
          <para>When using SSIS, avoid writing to intermediate work tables as much as possible. Read the data once and write the data once.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Appendix</title>
    <content>
      <para>Following are the full URLs for the hyperlinked text.</para>
      <para>
        <superscript>1</superscript> The Data Loading Performance Guide <externalLink><linkText>http://msdn.microsoft.com/library/dd425070.aspx</linkText><linkUri>http://msdn.microsoft.com/library/dd425070.aspx</linkUri></externalLink></para>
      <para>
        <superscript>2</superscript> SQL Server Best Practices Article  <externalLink><linkText>http://technet.microsoft.com/library/cc966380.aspx</linkText><linkUri>http://technet.microsoft.com/library/cc966380.aspx</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> Bulk Loading into a Table with Concurrent Queries  <externalLink><linkText>http://sqlcat.com/te</linkText><linkUri>http://sqlcat.com/technicalnotes/archive/2009/04/06/bulk-loading-data-into-a-table-with-concurrent-queries.aspx</linkUri></externalLink></para>
      <para>
        <superscript>4</superscript> Project REAL: Business Intelligence ETL Design Practices <externalLink><linkText>http:/</linkText><linkUri>http://www.microsoft.com/downloads/en/details.aspx?FamilyID=705B03F3-1BBF-417F-9E63-92A00A4744E6</linkUri></externalLink></para>
      <para>
        <superscript>5</superscript> SSIS Design Pattern – Incremental Loads <externalLink><linkText>http://sqlblog.com/blogs/</linkText><linkUri>http://sqlblog.com/blogs/andy_leonard/archive/2007/07/09/ssis-design-pattern-incremental-loads.aspx</linkUri></externalLink></para>
      <para>
        <superscript>6</superscript> Improving Incremental Loads with Change Data Capture <externalLink><linkText>http://msdn.microsoft.com/library/bb895315.aspx</linkText><linkUri>http://msdn.microsoft.com/library/bb895315.aspx</linkUri></externalLink></para>
      <para>
        <superscript>7</superscript> How To Process Change Data Capture (CDC) in SQL Server Integration Services SSIS 2008  <externalLink><linkText>http://www.mssqltips.com/tip.asp?tip=1755</linkText><linkUri>http://www.mssqltips.com/tip.asp?tip=1755</linkUri></externalLink></para>
      <para>
        <superscript>8</superscript> Attunity Oracle-CDC for SSIS  <externalLink><linkText>http://www.attunity.com/oracle_cdc_for_ssis</linkText><linkUri>http://www.attunity.com/oracle_cdc_for_ssis</linkUri></externalLink></para>
      <para>
        <superscript>9</superscript> SSIS – Using a checksum to determine if a row has changed  <externalLink><linkText>http://www.ssistalk.com/2007/03/09/ssis-using-a-checksum-to-determine-if-a-row-has-changed/</linkText><linkUri>http://www.ssistalk.com/2007/03/09/ssis-using-a-checksum-to-determine-if-a-row-has-changed/</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>