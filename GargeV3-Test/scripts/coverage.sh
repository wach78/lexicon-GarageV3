#!/usr/bin/env bash
set -euo pipefail

TEST_PROJECT="GargeV3-Test.csproj"
TEST_RESULTS_DIR="TestResults"
REPORT_DIR="coverage-report"

if [ ! -f "$TEST_PROJECT" ]; then
    echo "ERROR: Test project not found: $TEST_PROJECT"
    echo ""
    echo "Run this script from the GargeV3-Test folder."
    exit 1
fi

echo "Using test project:"
echo "$TEST_PROJECT"
echo ""

echo "Checking Coverlet package..."
if ! grep -q "coverlet.collector" "$TEST_PROJECT"; then
    dotnet add "$TEST_PROJECT" package coverlet.collector
fi

echo "Checking ReportGenerator tool..."

if command -v reportgenerator >/dev/null 2>&1; then
    REPORTGENERATOR_COMMAND="reportgenerator"
else
    if [ ! -f ".config/dotnet-tools.json" ] && [ ! -f "dotnet-tools.json" ]; then
        dotnet new tool-manifest
    fi

    if ! dotnet tool list | grep -q "dotnet-reportgenerator-globaltool"; then
        dotnet tool install dotnet-reportgenerator-globaltool
    else
        dotnet tool restore
    fi

    REPORTGENERATOR_COMMAND="dotnet tool run reportgenerator --"
fi

echo "Cleaning old coverage output..."
rm -rf "$REPORT_DIR"
rm -rf "$TEST_RESULTS_DIR"

echo "Running tests with coverage..."
dotnet test "$TEST_PROJECT" --collect:"XPlat Code Coverage"

echo "Generating HTML coverage report..."
$REPORTGENERATOR_COMMAND \
    -reports:"$TEST_RESULTS_DIR/**/coverage.cobertura.xml" \
    -targetdir:"$REPORT_DIR" \
    -reporttypes:"Html;TextSummary"

echo ""
echo "Coverage summary:"
cat "$REPORT_DIR/Summary.txt" || true

echo ""
echo "Done."
echo "Open this file:"
echo "$REPORT_DIR/index.html"
