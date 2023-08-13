# Pact standalone executables

This package contains the Ruby implementations of the Pact Mock Service, Pact Provider Verifier and Pact Broker Client, packaged with Travelling Ruby so that they can be run from the command line without a Ruby installation.

To connect to a Pact Broker that uses custom SSL cerificates, set the environment variable `$SSL_CERT_FILE` or `$SSL_CERT_DIR` to a path that contains the appropriate certificate.

## Package contents

This version (1.88.77) of the Pact standalone executables package contains:

  * pact gem 1.60.0
  * pact-mock_service gem 3.9.0
  * pact-support gem 1.17.0
  * pact-provider-verifier gem 1.36.0
  * pact_broker-client gem 1.56.0
  * pact-message gem 0.11.1


## Usage

<a name="pact-mock-service"></a>
### pact-mock-service

```
Commands:
  pact-mock-service control               # Run a Pact mock service control s...
  pact-mock-service control-restart       # Start a Pact mock service control...
  pact-mock-service control-start         # Start a Pact mock service control...
  pact-mock-service control-stop          # Stop a Pact mock service control ...
  pact-mock-service help [COMMAND]        # Describe available commands or on...
  pact-mock-service restart               # Start or restart a mock service. ...
  pact-mock-service service               # Start a mock service. If the cons...
  pact-mock-service start                 # Start a mock service. If the cons...
  pact-mock-service stop -p, --port=PORT  # Stop a Pact mock service
  pact-mock-service version               # Show the pact-mock-service gem ve...

Usage:
  pact-mock-service service

Options:
      [--consumer=CONSUMER]                                      # Consumer name
      [--provider=PROVIDER]                                      # Provider name
  -p, [--port=PORT]                                              # Port on which to run the service
  -h, [--host=HOST]                                              # Host on which to bind the service
                                                                 # Default: localhost
  -d, [--pact-dir=PACT_DIR]                                      # Directory to which the pacts will be written
  -m, [--pact-file-write-mode=PACT_FILE_WRITE_MODE]              # `overwrite` or `merge`. Use `merge` when running multiple mock service instances in parallel for the same consumer/provider pair. Ensure the pact file is deleted before running tests when using this option so that interactions deleted from the code are not maintained in the file.
                                                                 # Default: overwrite
  -i, [--pact-specification-version=PACT_SPECIFICATION_VERSION]  # The pact specification version to use when writing the pact. Note that only versions 1 and 2 are currently supported.
                                                                 # Default: 2
  -l, [--log=LOG]                                                # File to which to log output
      [--log-level=LOG_LEVEL]                                    # Log level. Options are DEBUG INFO WARN ERROR
                                                                 # Default: DEBUG
  -o, [--cors=CORS]                                              # Support browser security in tests by responding to OPTIONS requests and adding CORS headers to mocked responses
      [--ssl], [--no-ssl]                                        # Use a self-signed SSL cert to run the service over HTTPS
      [--sslcert=SSLCERT]                                        # Specify the path to the SSL cert to use when running the service over HTTPS
      [--sslkey=SSLKEY]                                          # Specify the path to the SSL key to use when running the service over HTTPS

Start a mock service. If the consumer, provider and pact-dir options are provided, the pact will be written automatically on shutdown (INT).

```

<a name="pact-stub-service"></a>
### pact-stub-service

```
Usage:
  pact-stub-service PACT_URI ...

Options:
  -p, [--port=PORT]                        # Port on which to run the service
  -h, [--host=HOST]                        # Host on which to bind the service
                                           # Default: localhost
  -l, [--log=LOG]                          # File to which to log output
  -n, [--broker-username=BROKER_USERNAME]  # Pact Broker basic auth username
  -p, [--broker-password=BROKER_PASSWORD]  # Pact Broker basic auth password
  -k, [--broker-token=BROKER_TOKEN]        # Pact Broker bearer token (can also be set using the PACT_BROKER_TOKEN environment variable)
      [--log-level=LOG_LEVEL]              # Log level. Options are DEBUG INFO WARN ERROR
                                           # Default: DEBUG
  -o, [--cors=CORS]                        # Support browser security in tests by responding to OPTIONS requests and adding CORS headers to mocked responses
      [--ssl], [--no-ssl]                  # Use a self-signed SSL cert to run the service over HTTPS
      [--sslcert=SSLCERT]                  # Specify the path to the SSL cert to use when running the service over HTTPS
      [--sslkey=SSLKEY]                    # Specify the path to the SSL key to use when running the service over HTTPS

Description:
  Start a stub service with the given pact file(s) or directories. Pact URIs
  may be local file or directory paths, or HTTP. Include any basic auth details
  in the URL using the format https://USERNAME:PASSWORD@URI. Where multiple
  matching interactions are found, the interactions will be sorted by response
  status, and the first one will be returned. This may lead to some
  non-deterministic behaviour. If you are having problems with this, please
  raise it on the pact-dev google group, and we can discuss some potential
  enhancements. Note that only versions 1 and 2 of the pact specification are
  currently fully supported. Pacts using the v3 format may be used, however,
  any matching features added in v3 will currently be ignored.

```

<a name="pact-provider-verifier"></a>
### pact-provider-verifier

To connect to a Pact Broker that uses custom SSL cerificates, set the environment variable `$SSL_CERT_FILE` or `$SSL_CERT_DIR` to a path that contains the appropriate certificate.

```
Usage:
  pact-provider-verifier PACT_URL ... -h, --provider-base-url=PROVIDER_BASE_URL

Options:
  -h, --provider-base-url=PROVIDER_BASE_URL                                  # Provider host URL
  -c, [--provider-states-setup-url=PROVIDER_STATES_SETUP_URL]                # Base URL to setup the provider states at
      [--pact-broker-base-url=PACT_BROKER_BASE_URL]                          # Base URL of the Pact Broker from which to retrieve the pacts. Can also be set using the environment variable PACT_BROKER_BASE_URL.
  -n, [--broker-username=BROKER_USERNAME]                                    # Pact Broker basic auth username. Can also be set using the environment variable PACT_BROKER_USERNAME.
  -p, [--broker-password=BROKER_PASSWORD]                                    # Pact Broker basic auth password. Can also be set using the environment variable PACT_BROKER_PASSWORD.
  -k, [--broker-token=BROKER_TOKEN]                                          # Pact Broker bearer token. Can also be set using the environment variable PACT_BROKER_TOKEN.
      [--provider=PROVIDER]                                                  
      [--consumer-version-tag=TAG]                                           # Retrieve the latest pacts with this consumer version tag. Used in conjunction with --provider. May be specified multiple times.
      [--provider-version-tag=TAG]                                           # Tag to apply to the provider application version. May be specified multiple times.
      [--provider-version-branch=BRANCH]                                     # The name of the branch the provider version belongs to.
  -g, [--tag-with-git-branch], [--no-tag-with-git-branch]                    # Tag provider version with the name of the current git branch. Default: false
  -a, [--provider-app-version=PROVIDER_APP_VERSION]                          # Provider application version, required when publishing verification results
  -r, [--publish-verification-results], [--no-publish-verification-results]  # Publish verification results to the broker. This can also be enabled by setting the environment variable PACT_BROKER_PUBLISH_VERIFICATION_RESULTS=true
      [--enable-pending], [--no-enable-pending]                              # Allow pacts which are in pending state to be verified without causing the overall task to fail. For more information, see https://pact.io/pending
      [--custom-provider-header=CUSTOM_PROVIDER_HEADER]                      # Header to add to provider state set up and pact verification requests. eg 'Authorization: Basic cGFjdDpwYWN0'. May be specified multiple times.
      [--custom-middleware=FILE]                                             # Ruby file containing a class implementing Pact::ProviderVerifier::CustomMiddleware. This allows the response to be modified before replaying. Use with caution!
  -v, [--verbose=VERBOSE]                                                    # Verbose output. Can also be set by setting the environment variable VERBOSE=true.
  -f, [--format=FORMATTER]                                                   # RSpec formatter. Defaults to custom Pact formatter. Other options are json and RspecJunitFormatter (which outputs xml).
  -o, [--out=FILE]                                                           # Write output to a file instead of $stdout.
      [--wait=SECONDS]                                                       # The number of seconds to poll for the provider to become available before running the verification
                                                                             # Default: 0
      [--log-dir=LOG_DIR]                                                    # The directory for the pact.log file
      [--log-level=LOG_LEVEL]                                                # The log level
                                                                             # Default: debug
      [--fail-if-no-pacts-found], [--no-fail-if-no-pacts-found]              # If specified, will fail when no pacts are found

Description:
  The parameters used when fetching pacts dynamically from a Pact Broker are:
  
  --pact-broker-base-url (REQUIRED)
  --provider (REQUIRED)
  --broker-username/--broker-password or --broker-token
  --consumer-version-tag or --consumer-version-selector
  --enable-pending
  --include-wip-pacts-since
  
  To
  verify a pact at a known URL (eg. when a verification is triggered by a
  'contract content changed' webhook), pass in the pact URL(s) as the first
  argument(s) to the command, and do NOT set any of the other parameters apart
  from the base URL and credentials.
  
  To publish verification results for either of the above
  scenarios, set:
  
  --publish-verification-results (REQUIRED)
  --provider-app-version (REQUIRED)
  --provider-version-tag or --tag-with-git-branch
  
  
  Selectors: These are specified using JSON strings.
  The keys are 'tag' (the name of the consumer version tag), 'latest'
  (true|false), 'consumer', and 'fallbackTag'. For example '{\"tag\":
  \"master\", \"latest\": true}'. For a detailed explanation of selectors, see https://pact.io/selectors#consumer-version-selectors

```

<a name="pact-broker-client"></a>
### pact-broker client

To connect to a Pact Broker that uses custom SSL cerificates, set the environment variable `$SSL_CERT_FILE` or `$SSL_CERT_DIR` to a path that contains the appropriate certificate.

<a name="pact-broker-client-publish"></a>
#### publish

```
Usage:
  pact-broker publish PACT_DIRS_OR_FILES ... -a, --consumer-app-version=CONSUMER_APP_VERSION -b, --broker-base-url=BROKER_BASE_URL

Options:
  -a, --consumer-app-version=CONSUMER_APP_VERSION                                # The consumer application version
  -h, [--branch=BRANCH]                                                          # Repository branch of the consumer version
      [--auto-detect-version-properties], [--no-auto-detect-version-properties]  # Automatically detect the repository branch from known CI environment variables or git CLI.
  -t, [--tag=TAG]                                                                # Tag name for consumer version. Can be specified multiple times.
  -g, [--tag-with-git-branch], [--no-tag-with-git-branch]                        # Tag consumer version with the name of the current git branch. Default: false
      [--build-url=BUILD_URL]                                                    # The build URL that created the pact
      [--merge], [--no-merge]                                                    # If a pact already exists for this consumer version and provider, merge the contents. Useful when running Pact tests concurrently on different build nodes.
  -o, [--output=OUTPUT]                                                          # json or text
                                                                                 # Default: text
  -b, --broker-base-url=BROKER_BASE_URL                                          # The base URL of the Pact Broker
  -u, [--broker-username=BROKER_USERNAME]                                        # Pact Broker basic auth username
  -p, [--broker-password=BROKER_PASSWORD]                                        # Pact Broker basic auth password
  -k, [--broker-token=BROKER_TOKEN]                                              # Pact Broker bearer token
  -v, [--verbose], [--no-verbose]                                                # Verbose output. Default: false

Publish pacts to a Pact Broker.

```

<a name="pact-broker-client-can-i-deploy"></a>
#### can-i-deploy

```
Usage:
  pact-broker can-i-deploy -a, --pacticipant=PACTICIPANT -b, --broker-base-url=BROKER_BASE_URL

Options:
  -a, --pacticipant=PACTICIPANT            # The pacticipant name. Use once for each pacticipant being checked.
  -e, [--version=VERSION]                  # The pacticipant version. Must be entered after the --pacticipant that it relates to.
      [--ignore=IGNORE]                    # The pacticipant name to ignore. Use once for each pacticipant being ignored. A specific version can be ignored by also specifying a --version after the pacticipant name option.
  -l, [--latest=[TAG]]                     # Use the latest pacticipant version. Optionally specify a TAG to use the latest version with the specified tag.
      [--to-environment=ENVIRONMENT]       # The environment into which the pacticipant(s) are to be deployed
      [--to=TAG]                           # The tag that represents the branch or environment of the integrated applications for which you want to check the verification result status.
  -o, [--output=OUTPUT]                    # json or table
                                           # Default: table
      [--retry-while-unknown=TIMES]        # The number of times to retry while there is an unknown verification result (ie. the provider verification is likely still running)
                                           # Default: 0
      [--retry-interval=SECONDS]           # The time between retries in seconds. Use in conjuction with --retry-while-unknown
                                           # Default: 10
      [--dry-run], [--no-dry-run]          # When dry-run is enabled, always exit process with a success code. Can also be enabled by setting the environment variable PACT_BROKER_CAN_I_DEPLOY_DRY_RUN=true.
  -b, --broker-base-url=BROKER_BASE_URL    # The base URL of the Pact Broker
  -u, [--broker-username=BROKER_USERNAME]  # Pact Broker basic auth username
  -p, [--broker-password=BROKER_PASSWORD]  # Pact Broker basic auth password
  -k, [--broker-token=BROKER_TOKEN]        # Pact Broker bearer token
  -v, [--verbose], [--no-verbose]          # Verbose output. Default: false

Description:
  Returns exit code 0 or 1, indicating whether or not the specified application
  (pacticipant) has a successful verification result with each of the
  application versions that are already deployed to a particular environment.
  Prints out the relevant pact/verification details, indicating any missing or
  failed verification results.

  The can-i-deploy tool was originally written to support specifying versions
  and dependencies using tags. This usage has now been superseded by first
  class support for environments, deployments and releases. For documentation
  on how to use can-i-deploy with tags, please see
  https://docs.pact.io/pact_broker/client_cli/can_i_deploy_usage_with_tags/

  Before `can-i-deploy` can be used, the relevant environment resources must
  first be created in the Pact Broker using the `create-environment` command.
  The "test" and "production" environments will have been seeded for you. You
  can check the existing environments by running `pact-broker
  list-environments`. See
  https://docs.pact.io/pact_broker/client_cli/readme#environments for more
  information.

  $ pact-broker create-environment --name "uat" --display-name "UAT"
  --no-production

  After an application is deployed or released, its deployment must be recorded
  using the `record-deployment` or `record-release` commands. See
  https://docs.pact.io/pact_broker/recording_deployments_and_releases/ for more
  information.

  $ pact-broker record-deployment --pacticipant Foo --version 173153ae0
  --environment uat

  Before an application is deployed or released to an environment, the
  can-i-deploy command must be run to check that the application version is
  safe to deploy with the versions of each integrated application that are
  already in that environment.

  $ pact-broker can-i-deploy --pacticipant PACTICIPANT --version VERSION
  --to-environment ENVIRONMENT

  Example: can I deploy version 173153ae0 of application Foo to the test
  environment?

  $ pact-broker can-i-deploy --pacticipant Foo --version 173153ae0
  --to-environment test

  Can-i-deploy can also be used to check if arbitrary versions have a
  successful verification. When asking "Can I deploy this application version
  with the latest version from the main branch of another application" it
  functions as a "can I merge" check.

  $ pact-broker can-i-deploy --pacticipant Foo 173153ae0 \ --pacticipant Bar
  --latest main

```

<a name="pact"></a>
### pact

<a name="pact-docs"></a>
#### docs
```
Usage:
  pact docs

Options:
  [--pact-dir=PACT_DIR]  # Directory containing the pacts
                         # Default: /home/runner/work/pact-ruby-standalone/pact-ruby-standalone/build/tmp/spec/pacts
  [--doc-dir=DOC_DIR]    # Documentation directory
                         # Default: /home/runner/work/pact-ruby-standalone/pact-ruby-standalone/build/tmp/doc/pacts

Generate Pact documentation in markdown

```

### pact-message

```
Commands:
  pact-message help [COMMAND]                                                ...
  pact-message reify                                                         ...
  pact-message update MESSAGE_JSON --consumer=CONSUMER --pact-dir=PACT_DIR --...
  pact-message version                                                       ...


```