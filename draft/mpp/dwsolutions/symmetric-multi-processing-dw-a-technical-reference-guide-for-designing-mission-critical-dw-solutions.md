---
title: "Symmetric Multi-Processing (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 7d97c883-369c-4c79-a5f1-d6ae40660b96
caps.latest.revision: 3
manager: jeffreyg
---
# Symmetric Multi-Processing (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
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
    <para>Symmetric multi-processing (SMP) data warehousing (DW) is the most common architecture for data warehouses under 50 TB. These systems are characterized by a single instance of a RDBMS sharing all resources (CPU/Memory/Disk).</para>
    <para>All major RDBMS vendors (IBM/Oracle/Sybase/Microsoft) provide SMP versions of their RDBMS for Data Warehouse. </para>
    <para>As SMP-based DW implementations grow in terms of data size and query efficiency, it becomes more and more challenging to design and maintain efficient data warehouse infrastructure.</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>The following resources provide some general information and best practices.</para>
      <list class="bullet">
        <listItem>
          <para>
            <externalLink>
              <linkText>Top 10 Best Practices for Building a Large Scale Relational Data Warehouse</linkText>
              <linkUri>http://sqlcat.com/top10lists/archive/2008/02/06/top-10-best-practices-for-building-a-large-scale-relational-data-warehouse.aspx</linkUri>
            </externalLink>
            <superscript>1</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>SMP or MPP for Data Warehousing</linkText>
              <linkUri>http://www.information-management.com/issues/20020501/5129-1.html</linkUri>
            </externalLink>
            <superscript>2</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Building a Data Warehouse: With Examples in SQL Server</linkText>
              <linkUri>http://books.google.com/books?id=eBacaL61sa4C&amp;pg=PA43&amp;lpg=PA43&amp;dq=smp+data+warehouse&amp;source=bl&amp;ots=MFC5NhpPRr&amp;sig=kEFbpu7nGWZvnM8xvib14L_sFBs&amp;hl=en&amp;ei=Ul0BTbjWE4bBswbwsJ3nBA&amp;sa=X&amp;oi=book_result&amp;ct=result&amp;resnum=6&amp;ved=0CEIQ6AEwBTgK</linkUri>
            </externalLink>
            <superscript>3</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Top 10 SQL Server 2005 Performance Issues for Data Warehouse and Reporting Applications</linkText>
              <linkUri>http://technet.microsoft.com/library/cc917690.aspx</linkUri>
            </externalLink>
            <superscript>4</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Disk Partition Alignment Best Practices for SQL Server</linkText>
              <linkUri>http://technet.microsoft.com/library/dd758814%28SQL.100%29.aspx</linkUri>
            </externalLink>
            <superscript>5</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>SQLIO Disk Subsystem Benchmark Tool</linkText>
              <linkUri>http://www.microsoft.com/downloads/en/details.aspx?familyid=9A8B005B-84E4-4F24-8D65-CB53442D9E19&amp;displaylang=en</linkUri>
            </externalLink>
            <superscript>6</superscript>
          </para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Case Studies and References</title>
    <content>
      <para>Industry standards compliance examples and implementation guidelines include the following:</para>
      <list class="bullet">
        <listItem>
          <para>
            <externalLink>
              <linkText>Barnes and Noble, Inc.: Bookseller Gains Business Insight Across Sales Channels with New Data Warehouse</linkText>
              <linkUri>http://www.microsoft.com/casestudies/Case_Study_Detail.aspx?CaseStudyID=48839</linkUri>
            </externalLink>
            <superscript>7</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Hilton Hotels: Hilton Sets the Table for Increases in Catering Revenue with New Database Solution</linkText>
              <linkUri>http://www.microsoft.com/casestudies/Case_Study_Detail.aspx?CaseStudyID=49192</linkUri>
            </externalLink>
            <superscript>8</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Stein Mart: Department Store Chain Speeds Reporting, Saves $600,000 a Year in Technology Costs</linkText>
              <linkUri>http://www.microsoft.com/casestudies/Microsoft-Office-SharePoint-Server-2007/Stein-Mart/Department-Store-Chain-Speeds-Reporting-Saves-600-000-a-Year-in-Technology-Costs/4000007013</linkUri>
            </externalLink>
            <superscript>9</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Fast Track Data Warehouse 2.0 Architecture</linkText>
              <linkUri>http://technet.microsoft.com/library/dd459178(SQL.100).aspx</linkUri>
            </externalLink>
            <superscript>10</superscript>
          </para>
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
      <para>Following are the full URLs for the hyperlinked text.</para>
      <para>
        <superscript>1</superscript> Top 10 Best Practices for Building a Large Scale Relational Data Warehouse  <externalLink><linkText>http://</linkText><linkUri>http://sqlcat.com/top10lists/archive/2008/02/06/top-10-best-practices-for-building-a-large-scale-relational-data-warehouse.aspx</linkUri></externalLink><externalLink><linkText>sqlcat.com/top10lists/archive/2008/02/06/top-10-best-practices-for-building-a-large-scale-relational-data-warehouse.aspx</linkText><linkUri>http://sqlcat.com/top10lists/archive/2008/02/06/top-10-best-practices-for-building-a-large-scale-relational-data-warehouse.aspx</linkUri></externalLink></para>
      <para>
        <superscript>2</superscript> SMP or MPP for Data Warehousing  <externalLink><linkText>http://</linkText><linkUri>http://www.information-management.com/issues/20020501/5129-1.html</linkUri></externalLink><externalLink><linkText>www.information-management.com/issues/20020501/5129-1.html</linkText><linkUri>http://www.information-management.com/issues/20020501/5129-1.html</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> Building a Data Warehouse: With Examples in SQL Server  <externalLink><linkText>http://books.google.com/books?id=eBacaL61sa4C&amp;pg=PA43&amp;lpg=PA43&amp;dq=smp+data+warehouse&amp;source=bl&amp;ots=MFC5NhpPRr&amp;sig=kEFbpu7nGWZvnM8xvib14L_sFBs&amp;hl=en&amp;ei=Ul0BTbjWE4bBswbwsJ3nBA&amp;sa=X&amp;oi=book_result&amp;ct=result&amp;resnum=6&amp;ved=0CEIQ6AEwBTgK#v=onepage&amp;q=smp%20data%20warehouse&amp;f=false</linkText><linkUri>http://books.google.com/books?id=eBacaL61sa4C&amp;pg=PA43&amp;lpg=PA43&amp;dq=smp+data+warehouse&amp;source=bl&amp;ots=MFC5NhpPRr&amp;sig=kEFbpu7nGWZvnM8xvib14L_sFBs&amp;hl=en&amp;ei=Ul0BTbjWE4bBswbwsJ3nBA&amp;sa=X&amp;oi=book_result&amp;ct=result&amp;resnum=6&amp;ved=0CEIQ6AEwBTgK</linkUri></externalLink></para>
      <para>
        <superscript>4</superscript> Top 10 SQL Server 2005 Performance Issues for Data Warehouse and Reporting Applications  <externalLink><linkText>http://technet.microsoft.com/library/cc917690.aspx</linkText><linkUri>http://technet.microsoft.com/library/cc917690.aspx</linkUri></externalLink></para>
      <para>
        <superscript>5</superscript> Disk Partition Alignment Best Practices for SQL Server  <externalLink><linkText>http://technet.microsoft.com/library/dd758814%28SQL.100%29.aspx</linkText><linkUri>http://technet.microsoft.com/library/dd758814%28SQL.100%29.aspx</linkUri></externalLink></para>
      <para>
        <superscript>6</superscript> SQLIO Disk Subsystem Benchmark Tool  <externalLink><linkText>http://www.microsoft.com/downloads/en/details.aspx?familyid=9A8B005B-84E4-4F24-8D65-CB53442D9E19&amp;displaylang=en</linkText><linkUri>http://www.microsoft.com/downloads/en/details.aspx?familyid=9A8B005B-84E4-4F24-8D65-CB53442D9E19&amp;displaylang=en</linkUri></externalLink></para>
      <para>
        <superscript>7</superscript> Barnes and Noble, Inc.: Bookseller Gains Business Insight Across Sales Channels with New Data Warehouse  <externalLink><linkText>http://www.microsoft.com/casestudies/Case_Study_Detail.aspx?CaseStudyID=48839</linkText><linkUri>http://www.microsoft.com/casestudies/Case_Study_Detail.aspx?CaseStudyID=48839</linkUri></externalLink></para>
      <para>
        <superscript>8</superscript> Hilton Hotels: Hilton Sets the Table for Increases in Catering Revenue with New Database Solution  <externalLink><linkText>http://www.microsoft.com/casestudies/Case_Study_Detail.aspx?CaseStudyID=49192</linkText><linkUri>http://www.microsoft.com/casestudies/Case_Study_Detail.aspx?CaseStudyID=49192</linkUri></externalLink></para>
      <para>
        <superscript>9</superscript> Stein Mart: Department Store Chain Speeds Reporting, Saves $600,000 a Year in Technology Costs  <externalLink><linkText>http://www.microsoft.com/casestudies/Microsoft-Office-SharePoint-Server-2007/Stein-Mart/Department-Store-Chain-Speeds-Reporting-Saves-600-000-a-Year-in-Technology-Costs/4000007013</linkText><linkUri>http://www.microsoft.com/casestudies/Microsoft-Office-SharePoint-Server-2007/Stein-Mart/Department-Store-Chain-Speeds-Reporting-Saves-600-000-a-Year-in-Technology-Costs/4000007013</linkUri></externalLink></para>
      <para>
        <superscript>10</superscript> Fast Track Data Warehouse 2.0 Architecture  <externalLink><linkText>http://technet.microsoft.com/library/dd459178(SQL.100).</linkText><linkUri>http://technet.microsoft.com/library/dd459178(SQL.100).aspx</linkUri></externalLink><externalLink><linkText>aspx</linkText><linkUri>http://technet.microsoft.com/library/dd459178(SQL.100).aspx</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>