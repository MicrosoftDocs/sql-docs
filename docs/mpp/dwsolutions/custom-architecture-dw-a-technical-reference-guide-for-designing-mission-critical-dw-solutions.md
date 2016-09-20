---
title: "Custom Architecture (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: c1312347-0a06-4008-a4f2-58ebcb2a42ba
caps.latest.revision: 4
manager: jeffreyg
---
# Custom Architecture (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
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
    <para>Customer Microsoft SQL Server architectures for data warehousing (DW) cover a full range of configurations. Often, the configuration is predicated by the "deal" that can be made between the client and the HW Vendor, and not always on carefully analyzed, business-driven, balanced design based on DW workloads.</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>The following resources provide reference material and additional information.</para>
      <list class="bullet">
        <listItem>
          <para>
            <externalLink>
              <linkText>Hardware 101 for SQL Server DBAs</linkText>
              <linkUri>http://www.mssqltips.com/tip.asp?tip=1331</linkUri>
            </externalLink>
            <superscript>1</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Solid State Disk Drive Considerations for SQL Server</linkText>
              <linkUri>http://www.mssqltips.com/tip.asp?tip=1389</linkUri>
            </externalLink>
            <superscript>2</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Benchmarks</linkText>
              <linkUri>http://www.microsoft.com/sqlserver/2008/en/us/benchmarks.aspx</linkUri>
            </externalLink>
            <superscript>3</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>TPC-H - All Results - Sorted by Database Vendor Version 2 Results</linkText>
              <linkUri>http://www.tpc.org/tpch/results/tpch_results.asp?orderby=dbms</linkUri>
            </externalLink>
            <superscript>4</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Storage Top 10 Best Practices</linkText>
              <linkUri>http://sqlcat.com/top10lists/archive/2007/11/21/storage-top-10-best-practices.aspx</linkUri>
            </externalLink>
            <superscript>5</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Top 10 SQL Server 2005 Performance Issues for Data Warehouse and Reporting Applications</linkText>
              <linkUri>http://technet.microsoft.com/library/cc917690.aspx</linkUri>
            </externalLink>
            <superscript>6</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Disk Partition Alignment Best Practices for SQL Server</linkText>
              <linkUri>http://technet.microsoft.com/library/dd758814%28SQL.100%29.aspx</linkUri>
            </externalLink>
            <superscript>7</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>SQLIO Disk Subsystem Benchmark Tool</linkText>
              <linkUri>http://www.microsoft.com/downloads/en/details.aspx?familyid=9A8B005B-84E4-4F24-8D65-CB53442D9E19&amp;displaylang=en</linkUri>
            </externalLink>
            <superscript>8</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Strategies for Partitioning Relational Data Warehouses in Microsoft SQL Server</linkText>
              <linkUri>http://technet.microsoft.com/library/cc966457.aspx</linkUri>
            </externalLink>
            <superscript>9</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Best Practices for Data Warehousing with SQL Server 2008</linkText>
              <linkUri>http://msdn.microsoft.com/library/cc719165%28v=sql.100%29.aspx</linkUri>
            </externalLink>
            <superscript>10</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>High Performance Data Warehouse with SQL Server 2005</linkText>
              <linkUri>http://www.microsoft.com/downloads/en/details.aspx?FamilyID=8D708350-4867-4447-AF69-B9AF2C2769F9&amp;amp;displaylang=en</linkUri>
            </externalLink>
            <superscript>11</superscript>
          </para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Case Studies and References</title>
    <content>
      <para>This section provides questions and issues to consider when working with your customers.</para>
      <list class="bullet">
        <listItem>
          <para>Determine the customer’s hardware vendor of choice. </para>
        </listItem>
        <listItem>
          <para>Understand the customer’s data center status: </para>
          <list class="bullet">
            <listItem>
              <para>Availability to accommodate special HW for Data Warehouse.</para>
            </listItem>
            <listItem>
              <para>Will the DW have to share a SAN or can it use dedicated storage?</para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>Determine all connectivity requirements (e.g. connection managers, OLEDB, ODBC, JDBC, and so on).</para>
        </listItem>
        <listItem>
          <para>Understand source database design (Star Schema, normalized).</para>
        </listItem>
        <listItem>
          <para>Understand if the client will rely on Data Extract (e.g. BCP) versus ETL/ELT (e.g. SSIS).</para>
        </listItem>
        <listItem>
          <para>For BCP Extracts, determine where data will land:</para>
          <list class="bullet">
            <listItem>
              <para>Storage platform and architecture.</para>
            </listItem>
            <listItem>
              <para>Can extracts be multi-threaded?</para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>Determine ETL/ELT requirements with regards to:</para>
          <list class="bullet">
            <listItem>
              <para>Data volume.</para>
            </listItem>
            <listItem>
              <para>Data “latency” or timing (how frequently does data need to be loaded.)</para>
            </listItem>
            <listItem>
              <para>Transformation complexity (from simple table lookups to complex business-oriented /data enrichment transforms).</para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>Determine geographical proximity of SQL Server source and target systems.</para>
        </listItem>
        <listItem>
          <para>Understand characteristics of the target (is it another SQL Server instance, or a flat file, or another RDBMS or analytical platform (e.g. SSAS)?).</para>
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
          <para>Understand the DW workload in terms of data loading and query workloads.</para>
        </listItem>
        <listItem>
          <para>Determine data latency requirements in conjunction with query/load concurrency.</para>
        </listItem>
        <listItem>
          <para>Workload requirements may vary by user/time of day/ query type and must be governed to prevent exhausting the system for a single query.</para>
        </listItem>
        <listItem>
          <para>Understand data security (row-level, column-level) requirements.</para>
        </listItem>
        <listItem>
          <para>Understand High Availability/Disaster Recovery requirements.</para>
        </listItem>
        <listItem>
          <para>Determine database backup/restore requirements in terms of frequency/storage.</para>
        </listItem>
        <listItem>
          <para>Develop an ongoing maintenance strategy for performance monitoring and tuning.</para>
        </listItem>
        <listItem>
          <para>Determine the technology landscape in terms of legacy RDBMS systems, ETL tools, BI tools.</para>
        </listItem>
        <listItem>
          <para>Determine deployment strategy:</para>
          <list class="bullet">
            <listItem>
              <para>System Sizing Considerations</para>
            </listItem>
            <listItem>
              <para>System Configuration</para>
            </listItem>
            <listItem>
              <para>Software Installation/Configuration</para>
            </listItem>
            <listItem>
              <para>Physical DW Database Design</para>
            </listItem>
            <listItem>
              <para>Metadata Management</para>
            </listItem>
            <listItem>
              <para>Designing the ETL system</para>
            </listItem>
            <listItem>
              <para>Developing BI Applications</para>
            </listItem>
            <listItem>
              <para>Developing the security model</para>
            </listItem>
            <listItem>
              <para>Operations Management</para>
            </listItem>
            <listItem>
              <para>Managing Growth</para>
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
        <superscript>1</superscript> Hardware 101 for SQL Server DBAs  <externalLink><linkText>http</linkText><linkUri>http://www.mssqltips.com/tip.asp?tip=1331</linkUri></externalLink><externalLink><linkText>://</linkText><linkUri>http://www.mssqltips.com/tip.asp?tip=1331</linkUri></externalLink><externalLink><linkText>www.mssqltips.com/tip.asp?tip=1331</linkText><linkUri>http://www.mssqltips.com/tip.asp?tip=1331</linkUri></externalLink></para>
      <para>
        <superscript>2</superscript> Solid State Disk Drive Considerations for SQL Server  <externalLink><linkText>http://</linkText><linkUri>http://www.mssqltips.com/tip.asp?tip=1389</linkUri></externalLink><externalLink><linkText>www.mssqltips.com/tip.asp?tip=1389</linkText><linkUri>http://www.mssqltips.com/tip.asp?tip=1389</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> Benchmarks  <externalLink><linkText>http://</linkText><linkUri /></externalLink><externalLink><linkText>www.microsoft.com/sqlserver/2008/en/us/benchmarks.aspx</linkText><linkUri>http://www.microsoft.com/sqlserver/2008/en/us/benchmarks.aspx</linkUri></externalLink></para>
      <para>
        <superscript>4</superscript> TPC-H - All Results - Sorted by Database Vendor Version 2 Results  <externalLink><linkText>http://</linkText><linkUri>http://www.tpc.org/tpch/results/tpch_results.asp?orderby=dbms</linkUri></externalLink><externalLink><linkText>www.tpc.org/tpch/results/tpch_results.asp?orderby=dbms</linkText><linkUri>http://www.tpc.org/tpch/results/tpch_results.asp?orderby=dbms</linkUri></externalLink></para>
      <para>
        <superscript>5</superscript> Storage Tope 10 Best Practices  <externalLink><linkText>http://</linkText><linkUri>http://sqlcat.com/top10lists/archive/2007/11/21/storage-top-10-best-practices.aspx</linkUri></externalLink><externalLink><linkText>sqlcat.com/top10lists/archive/2007/11/21/storage-top-10-best-practices.aspx</linkText><linkUri>http://sqlcat.com/top10lists/archive/2007/11/21/storage-top-10-best-practices.aspx</linkUri></externalLink></para>
      <para>
        <superscript>6</superscript> Top 10 SQL Server 2005 Performance Issues for Data Warehouse and Reporting Applications  <externalLink><linkText>http://technet.microsoft.com/library/cc917690.aspx</linkText><linkUri>http://technet.microsoft.com/library/cc917690.aspx</linkUri></externalLink></para>
      <para>
        <superscript>7</superscript> Disk Partition Alignment Best Practices for SQL Server  <externalLink><linkText>http://technet.microsoft.com/library/dd758814%28SQL.100%29.aspx</linkText><linkUri>http://technet.microsoft.com/library/dd758814%28SQL.100%29.aspx</linkUri></externalLink></para>
      <para>
        <superscript>8</superscript> SQLIO Disk Subsystem Benchmark Tool  <externalLink><linkText>http://www.microsoft.com/downloads/en/details.aspx?familyid=9A8B005B-84E4-4F24-8D65-CB53442D9E19&amp;displaylang=en</linkText><linkUri>http://www.microsoft.com/downloads/en/details.aspx?familyid=9A8B005B-84E4-4F24-8D65-CB53442D9E19&amp;displaylang=en</linkUri></externalLink></para>
      <para>
        <superscript>9</superscript> Strategies for Partitioning Relational Data Warehouses in Microsoft SQL Server  <externalLink><linkText>http://technet.microsoft.com/en-us/library/cc966457.aspx</linkText><linkUri>http://technet.microsoft.com/library/cc966457.aspx</linkUri></externalLink></para>
      <para>
        <superscript>10</superscript> Best Practices for Data Warehousing with SQL Server 2008  <externalLink><linkText>http://msdn.microsoft.com/library/cc719165%28v=sql.100%29.aspx</linkText><linkUri>http://msdn.microsoft.com/library/cc719165%28v=sql.100%29.aspx</linkUri></externalLink></para>
      <para>
        <superscript>11</superscript> High Performance Data Warehouse with SQL Server 2005  <externalLink><linkText>http://www.microsoft.com/downloads/en/details.aspx?FamilyID=8D708350-4867-4447-AF69-B9AF2C2769F9&amp;amp;displaylang=en</linkText><linkUri>http://www.microsoft.com/downloads/en/details.aspx?FamilyID=8D708350-4867-4447-AF69-B9AF2C2769F9&amp;amp;displaylang=en</linkUri></externalLink></para>
      <para>
        <superscript>12</superscript> SQL Server 2008 hardware and software requirements  <externalLink><linkText>http://</linkText><linkUri>http://searchsystemschannel.techtarget.com/generic/0,295582,sid99_gci1372853,00.html</linkUri></externalLink><externalLink><linkText>searchsystemschannel.techtarget.com/generic/0,295582,sid99_gci1372853,00.html</linkText><linkUri>http://searchsystemschannel.techtarget.com/generic/0,295582,sid99_gci1372853,00.html</linkUri></externalLink></para>
      <para>
        <superscript>13</superscript> EMC Celerra Unified Storage Platforms: Reference Architecture  <externalLink><linkText>http://</linkText><linkUri>http://www.emc.com/collateral/hardware/technical-documentation/h2970-emc-celerra-ns20-iscsi-ref-arch.pdf</linkUri></externalLink><externalLink><linkText>www.emc.com/collateral/hardware/technical-documentation/h2970-emc-celerra-ns20-iscsi-ref-arch.pdf</linkText><linkUri>http://www.emc.com/collateral/hardware/technical-documentation/h2970-emc-celerra-ns20-iscsi-ref-arch.pdf</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>