using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lines : System.Object
{
  private Dictionary<string, Line> linesById;
  private Dictionary<string, List<Line>> linesByLocation;
  private int numAddedLines;

  public Lines() {
    this.linesById = new Dictionary<string, Line>();
    this.linesByLocation = new Dictionary<string, List<Line>>();
    this.numAddedLines = 0;
  }

  public string AddLine(Line line) {
    string id = this.numAddedLines.ToString();
    this.linesById.Add(id, line);
    this.numAddedLines += 1;
    this.AddLineByLocation(line);

    return id;
  }

  public Line GetLineById(string id) {
    return this.linesById[id];
  }

  public List<Line> GetLinesAtPosition(Position position) {
    if(this.linesByLocation.ContainsKey(position.ToString())) {
      return this.linesByLocation[position.ToString()];
    }
    return new List<Line>();
  }

  public void ForEach(System.Action<Line> action) {
    foreach(KeyValuePair<string, Line> entry in this.linesById) {
      action(entry.Value);
    }
  }

  public void UpdateLinePositions() {
    this.linesByLocation.Clear();
    foreach(KeyValuePair<string, Line> entry in this.linesById) {
      this.AddLineByLocation(entry.Value);
    }
  }

  private void AddLineByLocation(Line line) {
    if(this.linesByLocation.ContainsKey(line.getCurrentPosition().ToString())) {
      List<Line> lines = this.linesByLocation[line.getCurrentPosition().ToString()];
      lines.Add(line);
    } else {
      List<Line> lines = new List<Line>();
      lines.Add(line);
      this.linesByLocation.Add(line.getCurrentPosition().ToString(), lines);
    }
  }
}
