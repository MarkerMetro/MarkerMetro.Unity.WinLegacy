# How to Contribute

We love Pull Requests! Your contributions help make WinLegacy great.

So you want to contribute to WinLegacy. Great! Contributions take many forms
from submitting issues, writing docs, to making code changes. We welcome
it all. Don't forget to sign up for a [GitHub account](https://github.com/signup/free),
if you haven't already.

## Getting Started

You can clone this repository locally from GitHub using the "Clone in Desktop"
button from the main project site, or run this command in the Git Shell:

`git clone https://github.com/MarkerMetro/MarkerMetro.Unity.WinLegacy.git WinLegacy`

Make sure you have the correct line endings set on Windows

`git config --global core.autocrlf true`

If you want to make contributions to the project,
[forking the project](https://help.github.com/articles/fork-a-repo) is the
easiest way to do this. You can then clone down your fork instead:

`git clone https://github.com/MY-USERNAME-HERE/MarkerMetro.Unity.WinLegacy.git WinLegacy`

### How is the codebase organised?

The project's root folder contains the solution `MarkerMetro.Unity.WinLegacy` that
contain all the WinIntegration projects: Unity, Windows Store and Windows Phone,
along with their test projects.

For more information, take a look at the [README file](README.md).

WinLegacy contain implementations of some .NET classes that are commonly used
in Unity projects. The namespaces are organized following the .NET namespaces.
So for example, `System.Net.Sockets` becomes
`MarkerMetro.Unity.WinLegacy.Net.Sockets`.

Some classes that were provided (System.IO or System.Collections.ArrayList etc) in later 
versions of Unity are located under MarkerMetro.Unity.WinLegacy.Plugin namespace.

## Making Changes

When you're ready to make a change,
[create a branch](https://help.github.com/articles/fork-a-repo#create-branches)
off the `master` branch. We use `master` as the default branch for the
repository, and it holds the most recent contributions, so any changes you make
in master might cause conflicts down the track.

If you make focused commits (instead of one monolithic commit) and have descriptive
commit messages, this will help speed up the review process.

WinLegacy also has a suite of tests which you can run to ensure existing
behaviour is unchanged. If you're adding new features, please add some tests
alongside so the maintainers can sleep at night, knowing their safety blanket
is nice and green!

### Submitting Changes

You can publish your branch by running this command from the Git Shell:

`git push origin MY-BRANCH-NAME`

Once your changes are ready to be reviewed, publish the branch to GitHub and
[open a pull request](https://help.github.com/articles/using-pull-requests)
against it.

A few little tips with pull requests:

- prefix the title with `[WIP]` to indicate this is a work-in-progress. It's
always good to get feedback early, so don't be afraid to open the PR before it's "done".
- use [checklists](https://github.com/blog/1375-task-lists-in-gfm-issues-pulls-comments)
to indicate the tasks which need to be done, so everyone knows how close you are to done.
- add comments to the PR about things that are unclear or you would like suggestions on.

Don't forget to mention in the pull request description which issue/issues are
being addressed.

## Coding Conventions

Some things that will increase the chance that your pull request is accepted.

* Follow existing code conventions.
* Update the documentation, the surrounding one, examples elsewhere, guides,
whatever is affected by your contribution.

### MSDN Coding Guidelines

We follow the [MSDN Coding Guidelines](http://msdn.microsoft.com/en-us/library/ff926074.aspx)
for C# with a few exceptions and additions.

### Variable Naming Exception

We use an underscore for all private variables, whether instance or static.

### Unit Tests

We use the conventions established here in
[this article](http://osherove.com/blog/2005/4/3/naming-standards-for-unit-tests.html).

### Commit Messages

To write commit messages we follow the good practices detailed here in
[this blog post](http://chris.beams.io/posts/git-commit/).
