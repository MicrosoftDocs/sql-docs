---
title: "Real-Time Data Loading (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: e138f26a-f6c8-472b-bd5f-3cd9f48e6fd1
caps.latest.revision: 5
manager: jeffreyg
---
# Real-Time Data Loading (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
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
    <para>Data warehouse industry pundits have long argued that "real-time data loading cannot happen in data warehousing." With ever-growing business pressure from TELCO’s, financial institutions, operations management, and so on, we have to provide access to real-time data in the data warehouse. Depending on the hardware platform, the strategy used to capture/load the data can be radically different than the "conventional" method (e.g. transformations, dimensional lookups to load facts, and so on). There are many products on the market that offer a complex (and expensive) technology "stack" to capture data and load in real-time.</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>The following resources provide reference material and additional information.</para>
      <list class="bullet">
        <listItem>
          <para>The "near real time" latency is a very important factor. You can generally get to around one minute with Integration Services—after that you have to consider other load mechanisms (.NET applications and/or StreamInsight).</para>
        </listItem>
        <listItem>
          <para>Real time need NOT be a push model. A "busy loading" model may be used instead. It can bring you down into the minute range if you just loop and check for incoming data.</para>
        </listItem>
        <listItem>
          <para>Must use technologies such as RCSI for SQL Server and ROLAP for analysis services. Design of database and cube becomes less tolerant of errors when going real time resulting in increased risk of failure. One must cherry-pick best implementers and partners as there is no room for "learning on the job" when building the team.</para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Real-time data warehouse loading methodology</linkText>
              <linkUri>http://portal.acm.org/citation.cfm?id=1451949</linkUri>
            </externalLink>
            <superscript>1</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Considerations for Building a Real-time Data Warehouse</linkText>
              <linkUri>http://www.grcdi.nl/considerations.pdf</linkUri>
            </externalLink>
            <superscript>2</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>SQL Server 2008 R2 – StreamInsight</linkText>
              <linkUri>http://www.microsoft.com/sqlserver/2008/en/us/r2-complex-event.aspx</linkUri>
            </externalLink>
            <superscript>3</superscript>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Creating a Real Time Data Warehouse</linkText>
              <linkUri>http://www.andrewscg.com/pdfs/Creating_RealTime_DW.pdf</linkUri>
            </externalLink>
            <superscript>4</superscript>
          </para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Case Studies and References</title>
    <content>
      <para>Examples of successful architectures are described in the following case studies and white papers:</para>
      <list class="bullet">
        <listItem>
          <para>
            <externalLink>
              <linkText>Speeding Up SSIS Bulk Inserts into SQL Server</linkText>
              <linkUri>http://henkvandervalk.com/speeding-up-ssis-bulk-inserts-into-sql-server</linkUri>
            </externalLink>
            <superscript>5</superscript>
            <externalLink>
              <linkText />
              <linkUri />
            </externalLink>
          </para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>StreamInsight Case Study</linkText>
              <linkUri>http://blogs.msdn.com/b/streaminsight/archive/2010/10/11/streaminsight-case-study.aspx</linkUri>
            </externalLink>
            <superscript>6</superscript>
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
          <para>Consider positioning of StreamInsight as the primary platform to capture/load data realtime.</para>
        </listItem>
        <listItem>
          <para>Identify and understand requirements for real-time data loads versus near real-time: <externalLink><linkText>Near real-time</linkText><linkUri>http://en.wikipedia.org/wiki/Near_real-time</linkUri></externalLink>.<superscript>7</superscript></para>
        </listItem>
        <listItem>
          <para>If near real-time is acceptable, consider Change Data Capture (detailed in Incremental Data Loads). </para>
        </listItem>
        <listItem>
          <para>Based on accepted latency, consider loading the Output of a Package into Another Program. This is a SSIS feature where a client application can also read the output of a package directly from memory, without the need for an intermediate step that persists the data. See <externalLink><linkText>Loading the Output of a Package into Another Program</linkText><linkUri>http://msdn.microsoft.com/library/ms135917.aspx</linkUri></externalLink>.<superscript>8</superscript></para>
        </listItem>
        <listItem>
          <para>Understand the differences between a "push" and "pull" model. Real-time data is normally a "push" model.</para>
        </listItem>
        <listItem>
          <para>Understand customer’s position on Read-Committed versus Read uncommitted ("dirty reads"). </para>
        </listItem>
        <listItem>
          <para>Data capture/loading is often "event-driven." While there may be a steady stream of real-time date (e.g. web logs, trading feeds, operations monitoring) only certain events or combination of events (Complex Event Processing or "CEP") need be captured.</para>
        </listItem>
        <listItem>
          <para>Determine how to handle "catch-up" files or late-arriving data. This is sometimes referred to "out-of-sequence" processing and usually requires a table design (e.g. seq_id) to help you manage the sequence of data.</para>
        </listItem>
        <listItem>
          <para>Understand the impact on real-time data loads:</para>
          <list class="bullet">
            <listItem>
              <para>Impact on query performance</para>
            </listItem>
            <listItem>
              <para>Up-stream system requirements (data latency, transformation, and so on)</para>
            </listItem>
            <listItem>
              <para>Can data be loaded directly from sources (e.g. CDR data) or must it be heavily transformed (e.g. Insurance/Financial data)?</para>
            </listItem>
            <listItem>
              <para>Down-stream system requirements (e.g. for SSAS or SSRS cached reports, aggregations, enrichment, and so on)</para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>Understand Microsoft Integration Technologies for establishing most appropriate strategy for real-time data integrations (see <externalLink><linkText>Understanding Microsoft Integration Technologies</linkText><linkUri>http://technet.microsoft.com/library/dd879265(BTS.10).aspx</linkUri></externalLink><superscript>9</superscript>). </para>
        </listItem>
        <listItem>
          <para>Consider integration with Biz Talk for Real-Time Data Loads. See the <externalLink><linkText>Microsoft BizTalk Server site</linkText><linkUri>http://www.microsoft.com/biztalk/en/us/default.aspx</linkUri></externalLink><superscript>10</superscript> and <externalLink><linkText>Working With BizTalk Adapter for SQL Server</linkText><linkUri>http://msdn.microsoft.com/library/ms935658(BTS.10).aspx</linkUri></externalLink>.<superscript>11</superscript></para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Appendix</title>
    <content>
      <para>Following are the full URLs for the hyperlinked text:</para>
      <para>
        <superscript>1</superscript> Real-time data warehouse loading methodology  <externalLink><linkText>http://portal.acm.org/citation.cfm?id=1451949</linkText><linkUri>http://portal.acm.org/citation.cfm?id=1451949</linkUri></externalLink></para>
      <para>
        <superscript>2</superscript> Considerations for Building a Real-time Data Warehouse  <externalLink><linkText>http://www.grcdi.nl/considerations.pdf</linkText><linkUri>http://www.grcdi.nl/considerations.pdf</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> SQL Server 2008 R2 – StreamInsight  <externalLink><linkText>http://www.microsoft.com/sqlserver/2008/en/us/r2-complex-event.aspx</linkText><linkUri>http://www.microsoft.com/sqlserver/2008/en/us/r2-complex-event.aspx</linkUri></externalLink></para>
      <para>
        <superscript>4</superscript> Creating a Real Time Data Warehouse  <externalLink><linkText>http://www.andrewscg.com/pdfs/Creating_RealTime_DW.pdf</linkText><linkUri>http://www.andrewscg.com/pdfs/Creating_RealTime_DW.pdf</linkUri></externalLink></para>
      <para>
        <superscript>5</superscript> Speeding Up SSIS Bulk Inserts into SQL Server  <externalLink><linkText>http://henkvandervalk.com/speeding-up-ssis-bulk-inserts-into-sql-server</linkText><linkUri>http://henkvandervalk.com/speeding-up-ssis-bulk-inserts-into-sql-server</linkUri></externalLink></para>
      <para>
        <superscript>6</superscript> StreamInsight Case Study  <externalLink><linkText>http://blogs.msdn.com/b/streaminsight/archive/2010/10/11/streaminsight-case-study.aspx</linkText><linkUri>http://blogs.msdn.com/b/streaminsight/archive/2010/10/11/streaminsight-case-study.aspx</linkUri></externalLink></para>
      <para>
        <superscript>7</superscript> Near real-time  <externalLink><linkText>http://en.wikipedia.org/wiki/Near_real-time</linkText><linkUri>http://en.wikipedia.org/wiki/Near_real-time</linkUri></externalLink></para>
      <para>
        <superscript>8</superscript> Loading the Output of a Package into Another Program  <externalLink><linkText>http://msdn.microsoft.com/library/ms135917.aspx</linkText><linkUri>http://msdn.microsoft.com/library/ms135917.aspx</linkUri></externalLink></para>
      <para>
        <superscript>9</superscript> Understanding Microsoft Integration Technologies  <externalLink><linkText>http://technet.microsoft.com/library/dd879265(BTS.10).aspx</linkText><linkUri>http://technet.microsoft.com/library/dd879265(BTS.10).aspx</linkUri></externalLink></para>
      <para>
        <superscript>10</superscript> Microsoft BizTalk Server site  <externalLink><linkText>http://www.microsoft.com/biztalk/en/us/default.aspx</linkText><linkUri>http://www.microsoft.com/biztalk/en/us/default.aspx</linkUri></externalLink></para>
      <para>
        <superscript>11</superscript> Working With BizTalk Adapter for SQL Server <externalLink><linkText>http://msdn.microsoft.com/library/ms935658(BTS.10).aspx</linkText><linkUri>http://msdn.microsoft.com/library/ms935658(BTS.10).aspx</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>