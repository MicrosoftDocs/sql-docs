---
title: "Data Warehousing (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: b3dcc6d6-7b19-4ca9-aa5c-7f83095c70d5
caps.latest.revision: 4
manager: jeffreyg
---
# Data Warehousing (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
<?xml version="1.0" encoding="utf-8"?>
<developerConceptualDocument xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://ddue.schemas.microsoft.com/authoring/2003/5 http://clixdevr3.blob.core.windows.net/ddueschema/developer.xsd">
  <introduction>
    <table xmlns:caps="http://schemas.microsoft.com/build/caps/2013/11">
      <tbody>
        <tr>
          <TD>
            <para>
              <embeddedLabel>Want more guides like this one?</embeddedLabel> Go to <externalLink><linkText>Technical Reference Guides for Designing Mission-Critical Solutions</linkText><linkUri>http://technet.microsoft.com/en-us/sqlserver/hh273157</linkUri></externalLink>.</para>
          </TD>
        </tr>
      </tbody>
    </table>
    <para>Data warehouses are characterized by queries that scan larger numbers of rows, large ranges of data and may return relatively large results for the purposes of analysis and reporting. Data warehouses are also characterized by relatively large data loads versus small transaction-level inserts/updates/deletes. Database (DB) schemas are often de-normalized, and many times in the form of a star-schema. However, in the case of a hub-and-spoke architecture, the "hub" (also known as the operational data store [ODS]) is usually normalized, while the "spokes" (data marts) are de-normalized. </para>
    <para>While a relational database engine is at the heart of a data warehouse, most relational technologies are not tuned to deal with the large-query and data-loading workloads of data warehousing (DW). This is why we have developed special architectures (called reference architectures) to help the database engine work more efficiently with the hardware.</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>The following resources provide some general information and best practices.</para>
      <list class="bullet">
        <listItem>
          <para>
            <externalLink>
              <linkText>Data Warehouse Fundamentals</linkText>
              <linkUri>http://it.toolbox.com/wiki/index.php/Data_Warehouse_Fundamentals</linkUri>
            </externalLink>
            <superscript>1</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Articles: Data Warehouse Fundamentals</linkText>
              <linkUri>http://www.rkimball.com/html/articlesFundamental.html</linkUri>
            </externalLink>
            <superscript>2</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Inmoncif.com</linkText>
              <linkUri>http://www.inmoncif.com/home/</linkUri>
            </externalLink>
            <superscript>3</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Best Practices for Data Warehousing with SQL Server 2008</linkText>
              <linkUri>http://msdn.microsoft.com/library/cc719165.aspx</linkUri>
            </externalLink>
            <superscript>4</superscript>
          </para>
        </listItem>
        <listItem>
          <para>Understanding Concurrent Users versus Number of users: Concurrent users are defined as the number of users actively running tasks (usually queries) at any moment in time. Number of users usually refers to the total population of users having access to the database. Usually the number of users is a factor of 10 larger than the concurrent users. So if a customer claims to have 1000 users, it is acceptable to protract that number into 100 concurrent users for the purposes of capacity planning.</para>
        </listItem>
        <listItem>
          <para>Understanding mixed-query workloads: Queries can often be categorized into small, medium and large based on a number of criteria, but usually it is based on time. In most data warehouses, there is a mixed workload of fast-running versus long-running queries. In each case, it is important to determine this mix and to determine its frequency (hourly, daily, month-end, quarter-end, and so on). It is important to understand that the mixed query workload, coupled with concurrency, lead to proper capacity planning for a data warehouse.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Case Studies and References</title>
    <content>
      <para>SQL Server has been deployed for many Tier-1 and lower Tier applications by customers. Examples include:</para>
      <list class="bullet">
        <listItem>
          <para>
            <externalLink>
              <linkText>DWMantra.com</linkText>
              <linkUri>http://www.dwmantra.com/dwconcepts.html</linkUri>
            </externalLink>
            <superscript>5</superscript>
          </para>
        </listItem>
        <listItem>
          <para>Fast Track is a reference architecture with specific guidelines for Hardware and software to configure SQL Server for large-scale data warehouses &lt; 50TB in size. Note that Fast Track is NOT a product with a SKU, but is recommended, vendor-specific hardware configuration and best practices. <externalLink><linkText>Fast Track Reference Architecture</linkText><linkUri>http://sharepoint/sites/SSDWPU/default.aspx</linkUri></externalLink><superscript>6</superscript></para>
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
          <para>
            <externalLink>
              <linkText>Data Warehouse Maturity Level</linkText>
              <linkUri>http://sharepoint/sites/bpdcxstg/SQLCAT/Documents/Tier%201%20DW%20Toolbox/Data%20Warehouse%20Maturity%20Model.pptx</linkUri>
            </externalLink>
            <superscript>7</superscript>: Fully understand the customer’s data warehouse maturity level: are they just beginning (Basic) or do they have fully-managed DW’s and using data-governance (Dynamic).</para>
        </listItem>
        <listItem>
          <para>Determine if the client has a DW enterprise initiative or policy, or are they just trying to fulfill the needs of a specific project. This will govern the solutions we may present to the client (e.g. Fast Track or PDW).</para>
        </listItem>
        <listItem>
          <para>Understand the entire DW workload that is particular to the client. The workload is everything from loading data, to running DW queries, to reporting, to analytics and beyond. The depth and breadth of these components are often commensurate with the DW Maturity Level.</para>
        </listItem>
        <listItem>
          <para>Understand the data warehouse size, sizes of individual databases, incremental load volumes, concurrent users, and so on.</para>
        </listItem>
        <listItem>
          <para>Understand the nature of the DW queries; in other words, understand the client’s business. TECLO and investment bank customers tend to have less complex queries, but very large data-scan requirements. Conversely, Retailers, Transportation, Petrochemical customers have more complex queries. </para>
        </listItem>
        <listItem>
          <para>Understand the DW user-population mix: is it comprised of a large number of "Consumers" who want regularly-scheduled reports, or are there a few DW analysts running complex/elaborate queries requiring massive amounts of data?</para>
        </listItem>
        <listItem>
          <para>Understand the client’s hardware allegiance and acceptance of an appliance-based DW solution versus a hand-built/customized H/W solution.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Appendix</title>
    <content>
      <para>Following are the full URLs for the hyperlinked text.</para>
      <para>
        <superscript>1</superscript> Data Warehouse Fundamentals  <externalLink><linkText>http://it.toolbox.com/wiki/index.php/Data_Warehouse_Fundamentals</linkText><linkUri>http://it.toolbox.com/wiki/index.php/Data_Warehouse_Fundamentals</linkUri></externalLink></para>
      <para>
        <superscript>2</superscript> Articles: Data Warehouse Fundamentals  <externalLink><linkText>http://www.rkimball.com/html/articlesFundamental.html</linkText><linkUri>http://www.rkimball.com/html/articlesFundamental.html</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> Inmoncif.com  <externalLink><linkText>http://www.inmoncif.com/home</linkText><linkUri>http://www.inmoncif.com/home/</linkUri></externalLink></para>
      <para>
        <superscript>4</superscript> Best Practices for Data Warehousing with SQL Server 2008  <externalLink><linkText>http://msdn.microsoft.com/library/cc719165.aspx</linkText><linkUri>http://msdn.microsoft.com/library/cc719165.aspx</linkUri></externalLink></para>
      <para>
        <superscript>5</superscript> DWMantra.com  <externalLink><linkText>http://www.dwmantra.com/dwconcepts.html</linkText><linkUri>http://www.dwmantra.com/dwconcepts.html</linkUri></externalLink></para>
      <para>
        <superscript>6</superscript> Fast Track Reference Architecture  http://sharepoint/sites/SSDWPU/default.aspx" </para>
      <para>
        <superscript>7</superscript> IBM DB2 Driver for ODBC and CLI Overview  <externalLink><linkText>http://sharepoint/sites/bpdcxstg/SQLCAT/Documents/Tier%201%20DW%20Toolbox</linkText><linkUri>http://sharepoint/sites/bpdcxstg/SQLCAT/Documents/Tier%201%20DW%20Toolbox/Data%20Warehouse%20Maturity%20Model.pptx</linkUri></externalLink></para>
      <para />
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>