# Version history

## Version 2.1.0, released 2023-01-19

### New features

- Enable REST transport in C# ([commit 31e55cd](https://github.com/googleapis/google-cloud-dotnet/commit/31e55cdbafe12bfae68e28a75a1b75ceb445684f))

## Version 2.0.0, released 2022-06-08

This is the first version of this package to depend on GAX v4.

There are some breaking changes, both in GAX v4 and in the generated
code. The changes that aren't specific to any given API are [described in the Google Cloud
documentation](https://cloud.google.com/dotnet/docs/reference/help/breaking-gax4).
We don't anticipate any changes to most customer code, but please [file a
GitHub issue](https://github.com/googleapis/google-cloud-dotnet/issues/new/choose)
if you run into problems.

The most important change in this release is the use of the Grpc.Net.Client package
for gRPC communication, instead of Grpc.Core. When using .NET Core 3.1 or .NET 5.0+
this should lead to a smaller installation footprint and greater compatibility (e.g.
with Apple M1 chips). Any significant change in a core component comes with the risk
of incompatibility, however - so again, please let us know if you encounter any
issues.
## Version 1.2.0, released 2021-08-19

- [Commit ac367e2](https://github.com/googleapis/google-cloud-dotnet/commit/ac367e2): feat: Regenerate all APIs to support self-signed JWTs

## Version 1.1.0, released 2021-04-28

- [Commit 5fcc945](https://github.com/googleapis/google-cloud-dotnet/commit/5fcc945):
  - feat: Added support for configurable budget time period.
  - docs: Updated some documentation links.
- [Commit 6305ea0](https://github.com/googleapis/google-cloud-dotnet/commit/6305ea0): docs: Update rules for currency_code in budget_amount.

## Version 1.0.0, released 2020-11-18

First GA release.

- [Commit 5dc5761](https://github.com/googleapis/google-cloud-dotnet/commit/5dc5761): docs: Reworded comments for the new credit types filter
- [Commit c960a33](https://github.com/googleapis/google-cloud-dotnet/commit/c960a33): feat: Add support for credit type filter field.

## Version 1.0.0-beta01, released 2020-10-27

First beta release.


