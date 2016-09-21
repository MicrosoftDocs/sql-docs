---
title: "Data Sources (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 18500dab-add5-4f00-867f-ff265bcb3a19
caps.latest.revision: 3
manager: jeffreyg
---
# Data Sources (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
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
    <para>Data warehouses do not, generally speaking, create their own data. Instead they are always "fed" data from other sources—in many cases, from a myriad of sources. In a perfect world, data could be loaded directly from the sources into the DW, but that is seldom (if ever!) the case. Over 80% of a DW project is dedicated to the Extract, Transformation and Load (or more recently Extract, Load, then Transform), so it is imperative that data sources be carefully understood.</para>
    <para>Given the three data loading scenarios (initial, incremental, real-time), data source characteristics can present numerous challenges to the ETL/ELT process.</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>Following are some best practices:</para>
      <list class="bullet">
        <listItem>
          <para>Manage the data source identification process: </para>
          <list class="bullet">
            <listItem>
              <para>Identify Subject Matter Experts (SMEs). </para>
            </listItem>
            <listItem>
              <para>Identify dimension data sources. </para>
            </listItem>
            <listItem>
              <para>Identify fact data sources.</para>
            </listItem>
          </list>
        </listItem>
        <listItem>
          <para>When the major data sources have been identified it is time to quickly gain detailed understanding of each one:</para>
          <list class="bullet">
            <listItem>
              <para>Obtain existing documentation. </para>
            </listItem>
            <listItem>
              <para>Model and define the input. </para>
            </listItem>
            <listItem>
              <para>Profile the input. </para>
            </listItem>
            <listItem>
              <para>Improve data quality. </para>
            </listItem>
            <listItem>
              <para>Save results for further reuse.</para>
            </listItem>
          </list>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Case Studies and References</title>
    <content>
      <para>
        <externalLink>
          <linkText>Data Warehouse Architecture</linkText>
          <linkUri>http://www.1keydata.com/datawarehousing/data-warehouse-architecture.html</linkUri>
        </externalLink>
        <superscript>1</superscript> is a nice summary of the various data layers that may comprise a well-built data warehouse.</para>
    </content>
  </section>
  <section>
    <title>Questions and Considerations</title>
    <content>
      <para>This section provides questions and issues to consider when working with your customers.</para>
      <list class="bullet">
        <listItem>
          <para>What is the quality/cleanliness of the raw data?</para>
        </listItem>
        <listItem>
          <para>Are there any data profiling requirements?</para>
        </listItem>
        <listItem>
          <para>What are the data sources (flat file, XML, streaming, relational) and how reliable are they in terms of connectivity?</para>
        </listItem>
        <listItem>
          <para>Is there any special security requirements needed to access the data source(s)?</para>
        </listItem>
        <listItem>
          <para>Are there any "political" considerations (e.g. blockers) for getting access to the raw data sources?</para>
        </listItem>
        <listItem>
          <para>What are the metadata requirements (e.g. data lineage, transformation mapping, and so on)?</para>
        </listItem>
        <listItem>
          <para>Are there any Master Data considerations?</para>
        </listItem>
        <listItem>
          <para>What is the timeliness (aka "latency") of the data (instantaneous, every hour, daily, weekly, and so on)? How does the timeliness of the data compare to when the business needs the information?</para>
        </listItem>
        <listItem>
          <para>What is the complexity of data transformation? Is it a simple matter of surrogate key lookups or is there a significant amount of data cleansing and enriching that must occur?</para>
        </listItem>
        <listItem>
          <para>What is the proximity of the data feeds to the final data warehouse destination? Consider proximity of initial data versus ongoing incrementally loaded data.</para>
        </listItem>
        <listItem>
          <para>What is the disposition of the data once it has been loaded to the data warehouse? Is it simply discarded or must it be stored/archived in load-ready format?</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Appendix</title>
    <content>
      <para>Following are the full URLs for the hyperlinked text.</para>
      <para>
        <superscript>1</superscript> Data Warehouse Architecture  <externalLink><linkText>http://www.1keydata.com/datawarehousing/data-warehouse-architecture.html</linkText><linkUri>http://www.1keydata.com/datawarehousing/data-warehouse-architecture.html</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>