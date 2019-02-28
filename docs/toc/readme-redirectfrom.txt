
##  sql-docs-pr/docs/toc/readme-redirectfrom.txt


-------- PURPOSE --------


    The purpose of the 'docs/toc/redirectfrom-*.md' files is to enable 'extendBreadcrumb: true' to deliver a much better navigation experience for the Docs user.



-------- RULES --------


    Never link to any file that is named like 'redirectfrom-*.md', except on the 'href:' line of the one expando node that the particular 'redirectfrom-*.md' was created for.
    By "expando" node in the TOC, we meant a node that has an 'items:' line.    Never reference a 'redirectfrom-*.md' file from a non-expando node in the TOC.
    Never have any one 'redirectfrom-*.md' file be referenced by more than one toc.yml file, nor by less than one.    And never have any one 'redirectfrom-*.md' file be referenced more or less than one time within the one toc.yml that contains a reference to the file.


    If it ever comes time to delete a 'redirectfrom-*.md' file, do Not add a line for the file in the .op Redirection file.


    Keep all 'redirectfrom-*.md' files in the directory 'sql-docs-pr/docs/toc/', so that the Versioning system will never hide them.    The 'toc/' directory has all Versioning SQL Product & Versions assigned to it, in docs/docfx.json.


    If a 'redirectfrom-*.md' filename is used on an expando whose '- name:' is so simple or common that is or may likely become non-unique, then append a '-10' to its filename (immediately the '.md' extension).    If '-10' is already taken for that name, then next use '-11'.    Do not use numbers of only 1 or 3 digits.
    When in doubt about whether to append a two digit number, it seems safe to not use any number.    It is easy to add the number later if necessary, because these files can be deleted from GitHub without the usual need to add the file to the .op Redirectors .json file.



-------- docs/2014/ --------

    docs/toc/ has nothing to do with docs/2014/.    As of 2019/March, this whole docs/toc/ concerns only SQL Server versions 2016+ (and some Azure SQL Database etc).





Thanks.  GeneMi.  2019/02/28



// EOFile
