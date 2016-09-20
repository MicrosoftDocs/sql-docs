---
title: "Massively Parallel Processing (DW)---a Technical Reference Guide for Designing Mission-Crtical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f6931101-7a17-46f7-8578-e2794a0e07f1
caps.latest.revision: 3
manager: jeffreyg
---
# Massively Parallel Processing (DW)---a Technical Reference Guide for Designing Mission-Crtical DW Solutions
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
    <para>Almost all tier-one companies are likely to have data warehousing (DW) initiatives in place, and most will probably have the need for very large data warehouse (VLDW) capabilities. Most will also have reached a more mature level in the DW capability model and will appreciate the benefits of massively parallel processing (MPP) DW. </para>
    <para>Parallel data warehousing (PDW) takes data warehousing to new levels by introducing an MPP capability to Microsoft SQL Server. In simple terms, PDW extends the data capacity of SQL Server by distributing the data across multiple "nodes," which are separate and distinct instances of SQL Server. However, to the end-user, the MPP appears as one database. </para>
    <para>Furthermore, many of the same DW design techniques, query techniques apply, but without all of the complex DBA work (for example, capacity planning, storage management, and TEMPDB issues). The appliance model presents a comprehensive, fault-tolerant, less-expensive environment for VLDW applications making it an attractive augmentation to their SQL Server implementations.</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>The following resources provide reference material and additional information.</para>
      <list class="bullet">
        <listItem>
          <para>
            <externalLink>
              <linkText>Data warehouse appliance</linkText>
              <linkUri>http://en.wikipedia.org/wiki/Data_warehouse_appliance</linkUri>
            </externalLink>
            <superscript>1</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Getting Started with Parallel Data Warehouse</linkText>
              <linkUri>http://www.windowsitpro.com/article/business-intelligence/Getting-Started-with-Parallel-Data-Warehouse/5.aspx</linkUri>
            </externalLink>
            <superscript>2</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>SQL Server 2008 R2 Parallel Data Warehouse Demo</linkText>
              <linkUri>http://www.demomate.com/content/demos/SQL%20Server%202008%20R2%20Parallel%20Data%20Warehouse%20Demo.zip</linkUri>
            </externalLink>
            <superscript>3</superscript>
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
          <para>Fully understand the scalability and performance requirements.</para>
        </listItem>
        <listItem>
          <para>The client must have at least 20 TB or more of data to enjoy the benefits of PDW. </para>
        </listItem>
        <listItem>
          <para>The best PDW workloads are for very large table scans, near real-time data loads with heavy query concurrency, and well-developed query plans. </para>
        </listItem>
        <listItem>
          <para>Given the price-point of PDW (including maintenance and implementation), this technology should not be targeted to a simple project; unless that project has VLDW needs (e.g. Telco, Large Retail, Large Web Analytics (clickstream), and so on.</para>
        </listItem>
        <listItem>
          <para>Consider carefully the complexity of the client’s ETL/ELT and reporting requirements. PDW does not yet have all the T-SQL capabilities, stored procedures, UDF’s, and so on. normally seen in standard SQL Server implementations.</para>
        </listItem>
        <listItem>
          <para>Understand where the pain is for the client:</para>
          <list class="bullet">
            <listItem>
              <para>Query/Load Performance</para>
            </listItem>
            <listItem>
              <para>DW manageability/maintenance</para>
            </listItem>
            <listItem>
              <para>Cost of Ownership</para>
            </listItem>
            <listItem>
              <para>Scalability</para>
            </listItem>
            <listItem>
              <para>Fault tolerance (High Availability or "HA")</para>
            </listItem>
            <listItem>
              <para>Internal DW politics with the data center or H/W groups</para>
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
        <superscript>1</superscript> Data warehouse appliance  <externalLink><linkText>http://</linkText><linkUri>http://en.wikipedia.org/wiki/Data_warehouse_appliance</linkUri></externalLink><externalLink><linkText>en.wikipedia.org/wiki/Data_warehouse_appliance</linkText><linkUri>http://en.wikipedia.org/wiki/Data_warehouse_appliance</linkUri></externalLink></para>
      <para>
        <superscript>2</superscript> Getting Started with Parallel Data Warehouse  <externalLink><linkText>http</linkText><linkUri>http://www.windowsitpro.com/article/business-intelligence/Getting-Started-with-Parallel-Data-Warehouse/5.aspx</linkUri></externalLink><externalLink><linkText>://</linkText><linkUri>http://www.windowsitpro.com/article/business-intelligence/Getting-Started-with-Parallel-Data-Warehouse/5.aspx</linkUri></externalLink><externalLink><linkText>www.windowsitpro.com/article/business-intelligence/Getting-Started-with-Parallel-Data-Warehouse/5.aspx</linkText><linkUri>http://www.windowsitpro.com/article/business-intelligence/Getting-Started-with-Parallel-Data-Warehouse/5.aspx</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> SQL Server 2008 R2 Parallel Data Warehouse Demo  <externalLink><linkText>http://www.demomate.com/content/demos/SQL Server 2008 R2 Parallel Data Warehouse Demo.zip</linkText><linkUri>http://www.demomate.com/content/demos/SQL%20Server%202008%20R2%20Parallel%20Data%20Warehouse%20Demo.zip</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>