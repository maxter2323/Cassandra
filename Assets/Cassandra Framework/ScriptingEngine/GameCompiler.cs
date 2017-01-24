using UnityEngine;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text;
using System.IO;

public class GameCompiler
{

	public static Assembly Compile(string source, string path)
	{
		CSharpCodeProvider provider = new CSharpCodeProvider();
		CompilerParameters param = new CompilerParameters();
		foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
		{
			param.ReferencedAssemblies.Add(assembly.Location);
		}
		param.GenerateExecutable = false;
		param.GenerateInMemory = true;
		param.CompilerOptions = "/platform:anycpu";
		param.OutputAssembly = path;
		CompilerResults result = provider.CompileAssemblyFromSource(param, source);
		if (result.Errors.Count > 0) {
			var msg = new StringBuilder();
			foreach (CompilerError error in result.Errors) {
				Debug.Log("WOW: " + GetLine(source,  error.Line));
				msg.AppendFormat("Error ({0}) in {4}: LINE: {2},  COLUMN: {3} {1}\n",
					error.ErrorNumber, error.ErrorText, error.Line, error.Column, path);
			}
			Debug.Log("Error Source: " + source);
			throw new Exception(msg.ToString());
		}
		return result.CompiledAssembly;
	}

	//error.ErrorText
	public static string GetLine(string text, int lineNo)
	{
	  string[] lines = text.Replace("\r","").Split('\n');
	  return lines.Length >= lineNo ? lines[lineNo-1] : null;
	}

	public static byte[] CompileAsBytes(string sourceCode)
	{
		string fileName = Application.streamingAssetsPath + "/CassandraGame.dll";
		Assembly compiledCode = GameCompiler.Compile(sourceCode, fileName);
		byte[] bytes = File.ReadAllBytes(fileName);
		File.Delete(fileName);
		return bytes;
	}
}
