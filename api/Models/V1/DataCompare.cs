namespace DiffProject.Models.V1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using api.Models.V1;

    public class DataCompare
    {
        Data _leftData;
        Data _rightData;

        public List<DataCompareLine> LeftResult { get; private set; } = new List<DataCompareLine>();
        public List<DataCompareLine> RightResult { get; private set; } = new List<DataCompareLine>();

        public DataCompare(Data left, Data right)
        {
            _leftData = left;
            _rightData = right;
        }

        public DataCompare Compare()
        {
            var leftProcessedLines = new List<DataCompareLine>();
            var rightProcessedLines = new List<DataCompareLine>();

            if (_leftData.Equals(_rightData))
            {
                leftProcessedLines = _leftData
                    .Text
                    .Split('\n')
                    .Select((line, index) =>
                    {
                        return new DataCompareLine
                        {
                            LineIndex = index,
                            Line = line,
                            Match = true
                        };
                    })
                    .ToList();

                rightProcessedLines = leftProcessedLines;
            }
            else
            {
                var leftLines = _leftData.Text.Split('\n');
                var rightLines = _rightData.Text.Split('\n');

                var totalLineCount = leftLines.Length >= rightLines.Length ? leftLines.Length : rightLines.Length;

                var lastMatchedIndex = 0;

                var matchLeftLines = new Dictionary<int, int>(leftLines
                    .Select((line, leftIndex) =>
                    {
                        var rightIndex = Array.IndexOf(rightLines, line, lastMatchedIndex);

                        if (rightIndex >= 0)
                        {
                            lastMatchedIndex = rightIndex;

                            return new KeyValuePair<int, int>(leftIndex, rightIndex);
                        }

                        return new KeyValuePair<int, int>(leftIndex, -1);
                    })
                );

                lastMatchedIndex = 0;

                for (int line = 0; line < totalLineCount; line++)
                {
                    if (line < leftLines.Length)
                    {
                        var lineMatched = matchLeftLines[line];

                        if (lineMatched == lastMatchedIndex)
                        {
                            leftProcessedLines.Add(new DataCompareLine
                            {
                                LineIndex = line,
                                Line = leftLines[line],
                                Match = true
                            });
                            rightProcessedLines.Add(new DataCompareLine
                            {
                                LineIndex = lastMatchedIndex,
                                Line = rightLines[lastMatchedIndex],
                                Match = true
                            });

                            lastMatchedIndex++;
                        }
                        else if (lineMatched == -1)
                        {

                            leftProcessedLines.Add(new DataCompareLine
                            {
                                LineIndex = line,
                                Line = leftLines[line],
                                Match = false
                            });
                            rightProcessedLines.Add(new DataCompareLine
                            {
                                LineIndex = lineMatched,
                                Line = string.Empty,
                                Match = false
                            });
                        }
                        else if (lineMatched > lastMatchedIndex)
                        {
                            leftProcessedLines.AddRange(
                                Enumerable.Range(0, lineMatched - lastMatchedIndex)
                                .Select(x => new DataCompareLine
                                {
                                    LineIndex = -1,
                                    Line = string.Empty,
                                    Match = false
                                })
                            );

                            rightProcessedLines.AddRange(
                                Enumerable.Range(lastMatchedIndex, lineMatched - lastMatchedIndex)
                                .Select(x => new DataCompareLine
                                {
                                    LineIndex = x,
                                    Line = rightLines[x],
                                    Match = false
                                })
                            );

                            leftProcessedLines.Add(new DataCompareLine
                            {
                                LineIndex = line,
                                Line = leftLines[line],
                                Match = true
                            });

                            rightProcessedLines.Add(new DataCompareLine
                            {
                                LineIndex = lineMatched,
                                Line = rightLines[lineMatched],
                                Match = true
                            });

                            lastMatchedIndex = lineMatched + 1;
                        }
                    }
                }
            }

            this.LeftResult = leftProcessedLines;
            this.RightResult = rightProcessedLines;

            return this;
        }

        public struct DataCompareLine
        {
            public int LineIndex { get; set; }
            public string Line { get; set; }
            public bool Match { get; set; }
        }

    }
}
