namespace DiffProject.Models.V1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public struct DataCompare
    {
        public Data LeftData;
        public Data RightData;

        public List<DataCompareLine> LeftResult;
        public List<DataCompareLine> RightResult;

        public DataCompare(Data left, Data right)
        {
            LeftData = left;
            RightData = right;

            LeftResult = new List<DataCompareLine>();
            RightResult = new List<DataCompareLine>();
        }

        public DataCompare Compare()
        {
            var leftProcessedLines = new List<DataCompareLine>();
            var rightProcessedLines = new List<DataCompareLine>();

            if (LeftData.Equals(RightData))
            {
                leftProcessedLines = LeftData
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
                var leftLines = LeftData.Text.Split('\n');
                var rightLines = RightData.Text.Split('\n');

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
                                Line = rightLines[line],
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
