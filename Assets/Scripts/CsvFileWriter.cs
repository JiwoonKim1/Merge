using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;


public class CsvFileWriter : CsvFileCommon, IDisposable
{
	// Private members
	private StreamWriter Writer;
	private string OneQuote = null;
	private string TwoQuotes = null;
	private string QuotedFormat = null;

	/// <summary>
	/// Initializes a new instance of the CsvFileWriter class for the
	/// specified stream.
	/// </summary>
	/// <param name="stream">The stream to write to</param>
	///
	/// 
	public CsvFileWriter(Stream stream)
	{
		Writer = new StreamWriter(stream, Encoding.UTF8);
	}

	/// <summary>
	/// Initializes a new instance of the CsvFileWriter class for the
	/// specified file path.
	/// </summary>
	/// <param name="path">The name of the CSV file to write to</param>
	public CsvFileWriter(string path)
	{
		Writer = new StreamWriter(path, true, Encoding.UTF8);
	}

	/// <summary>
	/// Writes a row of columns to the current CSV file.
	/// </summary>
	/// <param name="columns">The list of columns to write</param>[[p
	public void WriteRow(List<string> columns)
	{
		// Verify required argument-0
		if (columns == null)
			throw new ArgumentNullException("columns");

		// Ensure we're using current quote character
		if (OneQuote == null || OneQuote[0] != Quote)
		{
			OneQuote = String.Format("{0}", Quote);
			TwoQuotes = String.Format("{0}{0}", Quote);
			QuotedFormat = String.Format("{0}{{0}}{0}", Quote);
		}

		// Write each column
		for (int i = 0; i < columns.Count; i++)
		{
			if (i > 0)
				Writer.Write(Delimiter);

			if (columns[i].IndexOfAny(SpecialChars) == -1)
				Writer.Write(columns[i]);
			else
				Writer.Write(QuotedFormat, columns[i].Replace(OneQuote, TwoQuotes));
		}
		Writer.WriteLine();
		Writer.Flush();
	}

	// Propagate Dispose to StreamWriter
	public void Dispose()
	{
		Writer.Close();
		Writer.Dispose();
	}
}