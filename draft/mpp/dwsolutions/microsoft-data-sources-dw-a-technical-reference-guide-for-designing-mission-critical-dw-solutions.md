---
title: "Microsoft Data Sources (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: d78c83b6-dcd5-4b8a-a223-5823c8c373cf
caps.latest.revision: 4
manager: jeffreyg
---
# Microsoft Data Sources (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
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
    <para>Microsoft SQL Server often serves as the source of both transactional (OLTP) data as well as existing data warehouse data. Key distinguishing features primarily focus on the connectivity to SQL Server as well as the geographical proximity of the SQL Server source to the Data Warehouse target. Other important items include volume, frequency of extract/load, complexity of data transformation, access windows to extract data. In addition to SQL Server, Microsoft Excel is often used as a source for data warehouses.</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>The following resources provide reference material and additional information.</para>
      <list class="bullet">
        <listItem>
          <para>
            <externalLink>
              <linkText>bcp Utility</linkText>
              <linkUri>http://msdn.microsoft.com/library/aa174646(v=sql.80).aspx</linkUri>
            </externalLink>
            <superscript>1</superscript> (specific syntax for BCP) </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Using the SQL Server Import and Export Wizard to Move Data</linkText>
              <linkUri>http://msdn.microsoft.com/library/ms141209.aspx</linkUri>
            </externalLink>
            <superscript>2</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Architecture of Integration Services</linkText>
              <linkUri>http://msdn.microsoft.com/library/bb522498.aspx</linkUri>
            </externalLink>
            <superscript>3</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Top 10 SQL Server Integration Services Best Practices</linkText>
              <linkUri>http://sqlcat.com/top10lists/archive/2008/10/01/top-10-sql-server-integration-services-best-practices.aspx</linkUri>
            </externalLink>
            <superscript>4</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Bulk Loading Data into a Table with Concurrent Queries</linkText>
              <linkUri>http://sqlcat.com/technicalnotes/archive/2009/04/06/bulk-loading-data-into-a-table-with-concurrent-queries.aspx</linkUri>
            </externalLink>
            <superscript>5</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Extract, transform, load</linkText>
              <linkUri>http://www.answers.com/topic/extract-transform-load</linkUri>
            </externalLink>
            <superscript>6</superscript>: Outstanding document covering all major aspects of ETL in a short, easy-to-understand format.</para>
        </listItem>
        <listItem>
          <para>When Excel is used as one of the sources, data is often provided in more than one Excel file, or the files get posted to network locations on specific schedules, having file names and other file properties potentially changed. <externalLink><linkText>How to: Loop through Excel Files and Tables by Using a Foreach Loop Container</linkText><linkUri>http://msdn.microsoft.com/library/ms345182.aspx</linkUri></externalLink><superscript>7</superscript> provides insight on how to provide for looping through Excel files as sources.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Case Studies and References</title>
    <content>
      <list class="bullet">
        <listItem>
          <para>
            <externalLink>
              <linkText>SSIS and Data Sources</linkText>
              <linkUri>http://social.technet.microsoft.com/wiki/contents/articles/ssis-and-data-sources.aspx</linkUri>
            </externalLink>
            <superscript>8</superscript> contains detail on various sources for SSIS.</para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Data Source (SSIS)</linkText>
              <linkUri>http://msdn.microsoft.com/library/ms141792.aspx</linkUri>
            </externalLink>
            <superscript>9</superscript> includes specifics of SSIS data sources.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Questions and Considerations</title>
    <content>
      <para>When dealing with Microsoft data sources following important points should be taken in consideration.</para>
      <list class="bullet">
        <listItem>
          <para>Determine accessibility to all data sources:</para>
          <list class="bullet">
            <listItem>
              <para>Are they online? If so, what is the latency (how frequently updated)? Are there "windows" of time that you will have to consider to access them?</para>
            </listItem>
            <listItem>
              <para>Are there any access security restrictions? Is access to SQL server provided via Windows or SQL authentication? If account used to access source data has privileges greater than reader ensure that security policies are defined and in place to prevent corruption of source data.</para>
            </listItem>
            <listItem>
              <para>How "load-ready" is the data (i.e. how much transformation/enrichment is required for loading to DW)?</para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>Determine all connectivity requirements (e.g. connection managers, OLEDB, ODBC, JDBC, and so on).</para>
        </listItem>
        <listItem>
          <para>Make sure that development, testing, staging, and production environments are established for proper development and deployment. For more detail please refer to TARG document ETL SSIS.</para>
        </listItem>
        <listItem>
          <para>Ensure that versions, service packs, and hotfixes for operating system and SQL Server are identical across environments.</para>
        </listItem>
        <listItem>
          <para>If SQL Server Destination in SSIS is used to bulk load data into SQL Server tables and views, keep in mind that SQL Server destination cannot be used in packages that access a SQL Server database on a remote server. In this case, packages should use OLE DB destination.</para>
        </listItem>
        <listItem>
          <para>Understand source database design (Star Schema, or normalized).</para>
        </listItem>
        <listItem>
          <para>Understand if source and destination operating systems and database engines run 32 or 64 bit versions of operating systems and SQL engine. This however, shouldn’t be an issue if directions under point 4 are implemented. For more detail please refer to "<externalLink><linkText>64 bit Considerations for Integration Services</linkText><linkUri>http://msdn.microsoft.com/library/ms141766.aspx</linkUri></externalLink>.<superscript>10</superscript></para>
        </listItem>
        <listItem>
          <para>Understand if the client will rely on Data Extract (e.g. BCP) versus ETL/ELT (e.g. SSIS).</para>
        </listItem>
        <listItem>
          <para>For BCP Extracts, determine where data will land:</para>
          <list class="bullet">
            <listItem>
              <para>Storage platform and architecture</para>
            </listItem>
            <listItem>
              <para>Can extracts be multi-threaded? To learn more about multi-threaded loads, see <externalLink><linkText>ETL Subsystem 31: Paralleling and Pipelining</linkText><linkUri>http://blog.todmeansfox.com/tag/multithread/</linkUri></externalLink>.<superscript>11</superscript></para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>Determine ETL/ELT requirements with regards to:</para>
          <list class="bullet">
            <listItem>
              <para>Data volume </para>
            </listItem>
            <listItem>
              <para>Impact of large data volumes to corporate network?</para>
            </listItem>
            <listItem>
              <para>Data "latency" or timing (how frequently does data need to be loaded)</para>
            </listItem>
            <listItem>
              <para>Transformation complexity (from simple table lookups to complex business-oriented /data enrichment transforms)</para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>Determine geographical proximity of SQL Server source and target systems.</para>
        </listItem>
        <listItem>
          <para>Understand characteristics of the target (is it another SQL Server instance, or a flat file, or another RDBMS or analytical platform (e.g. SSAS)?).</para>
        </listItem>
        <listItem>
          <para>Consider EDW architecture to include Staging (Landing) database, Operation Data Layer, Data Marts, and EDW. Also, assess if it is appropriate to manage common dimensions in a separate database (Conformed Dimensions). For more detail refer to slide 58 – ETL SSIS.</para>
        </listItem>
        <listItem>
          <para>For Excel sources consult the following MSDN references:  <externalLink><linkText>How to: Connect to an Excel Workbook</linkText><linkUri>http://msdn.microsoft.com/library/cc280527.aspx</linkUri></externalLink><superscript>12</superscript> and <externalLink><linkText>Excel Source</linkText><linkUri>http://msdn.microsoft.com/library/ms141683.aspx</linkUri></externalLink>.<superscript>13</superscript></para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Appendix</title>
    <content>
      <para>Following are the full URLs for the hyperlinked text.</para>
      <para>
        <superscript>1</superscript> bcp Utility  <link xlink:href="">http://msdn.microsoft.com/library/aa174646(v=sql.80)</link></para>
      <para>
        <superscript>2</superscript> Using the SQL Server Import and Export Wizard to Move Data  <link xlink:href="">http://msdn.microsoft.com/library/ms141209.aspx</link></para>
      <para>
        <superscript>3</superscript> Architecture of Integration Services  <link xlink:href="">http://msdn.microsoft.com/library/bb522498.aspx</link></para>
      <para>
        <superscript>4</superscript> Top 10 SQL Server Integration Services Best Practices  <externalLink><linkText>http://sqlcat.com/top10lists/archive/2008/10/01/top-10-sql-server-integration-services-best-practices.aspx</linkText><linkUri>http://sqlcat.com/top10lists/archive/2008/10/01/top-10-sql-server-integration-services-best-practices.aspx</linkUri></externalLink></para>
      <para>
        <superscript>5</superscript> Bulk Loading Data into a Table with Concurrent Queries  <externalLink><linkText>http://sqlcat.com/technicalnotes/archive/2009/04/06/bulk-loading-data-into-a-table-with-concurrent-queries.aspx</linkText><linkUri>http://sqlcat.com/technicalnotes/archive/2009/04/06/bulk-loading-data-into-a-table-with-concurrent-queries.aspx</linkUri></externalLink></para>
      <para>
        <superscript>6</superscript> Extract, transform, load  <link xlink:href="">http://www.answers.com/topic/extract-transform-load</link></para>
      <para>
        <superscript>7</superscript> How to: Loop through Excel Files and Tables by Using a Foreach Loop Container  <link xlink:href="">http://msdn.microsoft.com/library/ms345182.aspx</link></para>
      <para>
        <superscript>8</superscript> SSIS and Data Sources  <link xlink:href="">http://social.technet.microsoft.com/wiki/contents/articles/ssis-and-data-sources.aspx</link></para>
      <para>
        <superscript>9</superscript> Data Source (SSIS)  <link xlink:href="">http://msdn.microsoft.com/library/ms141792.aspx</link></para>
      <para>
        <superscript>10</superscript> 64-bit Considerations for Integration Services  <link xlink:href="">http://msdn.microsoft.com/library/ms141766.aspx</link></para>
      <para>
        <superscript>11</superscript> ETL Subsystem 31: Paralleling and Pipelining  <link xlink:href="">http://blog.todmeansfox.com/tag/multithread/</link></para>
      <para>
        <superscript>12</superscript> How to: Connect to an Excel Workbook  <link xlink:href="">http://msdn.microsoft.com/library/cc280527.aspx</link></para>
      <para>
        <superscript>13</superscript> Excel Source  <link xlink:href="">http://msdn.microsoft.com/library/ms141683.aspx</link></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>