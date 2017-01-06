---
title: "Reference Architecture (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 12987f51-3bd7-406a-92b9-d14f6400d008
caps.latest.revision: 4
manager: jeffreyg
---
# Reference Architecture (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
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
    <para>Microsoft SQL Server Fast Track was created to provide specific reference architecture, along with a set of best practices, to eliminate efforts associated with designing a SQL Server data warehouse. Extensive research into balancing CPU/Memory/Disk Storage with respects to SMP was done to determine the best configuration for data warehousing (DW)-related workloads.</para>
    <para>Fast Track is not a different version of SQL Server; instead it is a strict hardware configuration. Guidelines for loading data, minimizing fragmentation, and so on are also provided.</para>
    <para>Fast Track provides a far more cost-effective approach to implementing larger Data Warehouses (&lt;50 TB) in SMP SQL Server than if the clients had to design/manage the configuration themselves.</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>The following section provides some advice and references for best practices.</para>
      <list class="bullet">
        <listItem>
          <para>
            <externalLink>
              <linkText>An Introduction to Fast Track Data Warehouse Architectures</linkText>
              <linkUri>http://msdn.microsoft.com/library/dd459146%28SQL.100%29.aspx</linkUri>
            </externalLink>
            <superscript>1</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>MS BI / Data Warehousing Hardware Estimation Tools</linkText>
              <linkUri>http://siddhumehta.blogspot.com/2010/07/ms-bi-data-warehousing-hardware.html</linkUri>
            </externalLink>
            <superscript>2</superscript>
          </para>
        </listItem>
        <listItem>
          <para>The Fast Track reference architecture (RA) is VERY SPECIFIC and must be followed to the letter. Be wary of anybody trying to second-guess the RA. If changes of any kind are made (for example, different drives, more CPU’s, and so on), performance will be impacted.</para>
        </listItem>
        <listItem>
          <para>The FT architecture is designed to provide sequential I/O as much as possible; therefore, indexing should be used very judiciously to avoid introducing too much random I/O.</para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Estimating the Size of a Clustered Index</linkText>
              <linkUri>http://msdn.microsoft.com/library/ms178085.aspx</linkUri>
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
      <para>The following sample can be used for reference.</para>
      <list class="bullet">
        <listItem>
          <para>HP Fast Track architectures and customer examples: <externalLink><linkText>HP Fast Track Solutions for Microsoft SQL Server</linkText><linkUri>http://h71028.www7.hp.com/enterprise/cache/503252-0-0-0-121.html?jumpid=ex_r2858_w1/en/large/tsg/solutions_microsoft_fasttrack</linkUri></externalLink><superscript>4</superscript></para>
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
          <para>Understand Client’s query mix or the characteristics of their queries to ensure that the Fast-track architecture applies. FT enjoys large-type queries versus non-clustered index queries.</para>
        </listItem>
        <listItem>
          <para>Determine Client’s hardware vendor of choice and contact that vendor regarding their Fast Track hardware solutions.</para>
        </listItem>
        <listItem>
          <para>Understand the client’s Data Warehouse Maturity Level and Strategy.</para>
        </listItem>
        <listItem>
          <para>Understand data loading requirements (volumes, latency) and match them to recommended best practices for FT.</para>
        </listItem>
        <listItem>
          <para>Determine data growth maintenance strategy:</para>
          <list class="bullet">
            <listItem>
              <para>Managing fragmentation</para>
            </listItem>
            <listItem>
              <para>Monitoring/Managing indexes</para>
            </listItem>
            <listItem>
              <para>Backup/Restore Policies</para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>What is the query workload? Do most queries tend to perform large table-scans or are they more restricted to a few rows? This will determine partitioning/indexing strategies.</para>
        </listItem>
        <listItem>
          <para>What is the concurrency workload? Can data be loaded on a quiesced system, or must loads run while queries are in flight?</para>
        </listItem>
        <listItem>
          <para>Consider data growth and scale up/scale out requirements for the future. Ensure that the current design does not inhibit rapid data growth.</para>
        </listItem>
        <listItem>
          <para>If data are loading concurrently with queries, what partition switching strategies should be used to minimize performance impact?</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Appendix</title>
    <content>
      <para>Following are the full URLs for the hyperlinked text:</para>
      <para>
        <superscript>1</superscript> An Introduction to Fast Track Data Warehouse Architectures  <externalLink><linkText>http://msdn.microsoft.com/library/dd459146%28SQL.100%29.aspx</linkText><linkUri>http://msdn.microsoft.com/en-us/library/dd459146%28SQL.100%29.aspx</linkUri></externalLink></para>
      <para>
        <superscript>2</superscript> MS BI / Data Warehousing Hardware Estimation Tools  <externalLink><linkText>http://siddhumehta.blogspot.com/2010/07/ms-bi-data-warehousing-hardware.html</linkText><linkUri>http://siddhumehta.blogspot.com/2010/07/ms-bi-data-warehousing-hardware.html</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> Estimating the Size of a Clustered Index  <externalLink><linkText>http://</linkText><linkUri>http://msdn.microsoft.com/library/ms178085.aspx</linkUri></externalLink><externalLink><linkText>msdn.microsoft.com/library/ms178085.aspx</linkText><linkUri>http://msdn.microsoft.com/library/ms178085.aspx</linkUri></externalLink></para>
      <para>
        <superscript>4</superscript> HP Fast Track Solutions for Microsoft SQL Server  <externalLink><linkText>http://</linkText><linkUri>http://h71028.www7.hp.com/enterprise/cache/503252-0-0-0-121.html?jumpid=ex_r2858_w1/en/large/tsg/solutions_microsoft_fasttrack</linkUri></externalLink><externalLink><linkText>h71028.www7.hp.com/enterprise/cache/503252-0-0-0-121.html?jumpid=ex_r2858_w1/en/large/tsg/solutions_microsoft_Fast Track</linkText><linkUri>http://h71028.www7.hp.com/enterprise/cache/503252-0-0-0-121.html?jumpid=ex_r2858_w1/en/large/tsg/solutions_microsoft_fasttrack</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>