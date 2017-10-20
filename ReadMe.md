## Microsoft Open Source Code of Conduct

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

# Microsoft Technical Documentation Contributor Guide
You've found the GitHub repository that houses the source for the technical documentation published on [https://docs.microsoft.com](https://docs.microsoft.com).

This repository also contains guidance to help you contribute to our technical documentation. For a list of the articles in the contributors' guide, see [the index](https://github.com/MicrosoftDocs/azure-docs/blob/master/contributor-guide/contributor-guide-index.md).

## Contribute to documentation

* [Ways to contribute](#ways-to-contribute)
* [Code of conduct](#code-of-conduct)
* [About your contributions to content](#about-your-contributions-to-azure-content)
* [Repository organization](#repository-organization)
* [Use GitHub, Git, and this repository](#use-github-git-and-this-repository)
* [How to use markdown to format your topic](#how-to-use-markdown-to-format-your-topic)
* [More resources](#more-resources)
* [Index of all contributors' guide articles](contributor-guide/contributor-guide-index.md) (opens new page)

## Ways to contribute

* GitHub user interface. Either find the article in this repository, or visit the article on [https://docs.microsoft.com](https://docs.microsoft.com) and click the Edit link in upper right hand corner of the topic. That takes you to the the article file in GitHub.
* For substantial changes, adding or changing images, or contributing a new article, you need to fork this repository, install Git Bash, Markdown Pad, and learn some git commands.

## About your contributions to content
### Minor corrections
Minor corrections or clarifications you submit for documentation and code examples in this repo are covered by the [docs.microsoft.com Terms of Use](https://docs.microsoft.com/legal/termsofuse).

### Larger submissions
If you submit a pull request with significant changes to documentation and code examples or new content, we'll send a comment in GitHub asking you to submit an online Contribution License Agreement (CLA) if you are not an employee of Microsoft. We need you to complete the online form before we can accept your pull request.

## Use GitHub, Git, and this repository
For information about how to contribute, how to use the GitHub user interface to contribute small changes, and how to fork and clone the repository for more significant contributions, see [Install and set up tools for authoring in GitHub](https://github.com/MicrosoftDocs/azure-docs/blob/master/contributor-guide/tools-and-setup.md).

If you install GitBash and work locally, the steps for creating a new local working branch, making changes, and submitting the changes back to the main branch are listed in [Git commands for creating a new article or updating an existing article](https://github.com/MicrosoftDocs/azure-docs/blob/master/contributor-guide/git-commands-for-master.md)

### Branches
Create local working branches that target a specific scope of change. Each branch should be limited to a single concept/article both to streamline work flow and reduce the possibility of merge conflicts. Appropriate scope for a new branch:

* A new article (and associated images)
* Spelling and grammar edits on an article.
* Applying a single formatting change across a large set of articles (e.g. new copyright footer).

## How to use markdown to format your topic
All the articles in this repository use GitHub flavored markdown. [Markdown basics](https://help.github.com/articles/getting-started-with-writing-and-formatting-on-github/)


## Article metadata
Article metadata enables certain functionalities such as author and contributor attribution, breadcrumbs, article descriptions, and SEO optimizations as well as reporting Microsoft uses to evaluate content performance. [Here's the guidance for making sure your metadata is done right](contributor-guide/article-metadata.md).

### Labels
Automated labels are assigned to pull requests to help us manage the pull request workflow and to help let you know what's going on with your pull request:

* Contribution License Agreement related
  * cla-not-required: The change is relatively minor and does not require that you sign a CLA.
  * cla-required: The scope of the change is relatively large and requires that you sign a CLA.
  * cla-signed: The contributor signed the CLA, so the pull request can now move forward for review.
* Pillar labels: Labels such as PnP, Modern Apps, and TDC help categorize the pull requests by the internal organization that needs to review the pull request.
* Change sent to author: The author has been notified of the pending pull request.

## More resources
See the [index of our contributor's guide](https://github.com/MicrosoftDocs/azure-docs/blob/master/contributor-guide/contributor-guide-index.md) for all our guidance topics.

