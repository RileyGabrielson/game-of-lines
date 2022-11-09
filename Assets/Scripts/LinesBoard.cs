using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinesBoard : MonoBehaviour
{
  public GameObject lineView;
  public Transform linesParent;
  public float processDelay = 1f;

  private Lines lines;
  private List<GameObject> lineViews = new List<GameObject>();
  private float timer;
  private bool autoProcess = false;

  private void Start() {
    this.lines = new Lines();
    timer = 0f;
  }

  private void Update() {
    if(autoProcess) {
      timer -= Time.deltaTime;
      if(timer < 0f) {
        this.ProcessMove();
        timer = processDelay;
      }
    }
  }

  public void AutoProcess() {
    this.autoProcess = true;
  }

  public void StopAutoProcess() {
    this.autoProcess = false;
  }
    

  public void AddLine(Line line) {
    string id = this.lines.AddLine(line);
    Position position = line.getCurrentPosition();
    GameObject spawnedLineView = Instantiate(lineView, new Vector3(position.x, position.y, 0f), Quaternion.identity);
    spawnedLineView.GetComponent<LineView>().SetId(id);
    spawnedLineView.GetComponent<LineView>().SetDirection(line.getDirection());
    spawnedLineView.GetComponent<LineView>().UpdateTarget(new Vector3(position.x, position.y, 0f));
    this.lineViews.Add(spawnedLineView);
  }

  public void ProcessMove() {
    List<Line> newLines = new List<Line>();
    this.lines.ForEach(line => {
      if(line.canReproduce(this.lines)) {
        Line newLine = line.reproduce();
        newLines.Add(newLine);
      }
    });
    newLines.ForEach(line => this.AddLine(line));

    this.lines.ForEach(line => line.stageNextMove(this.lines));

    this.lines.ForEach(line => line.moveToStagedPosition(this.lines));
    this.lines.UpdateLinePositions();
    this.lineViews.ForEach(lineView => {
      lineView.GetComponent<LineView>().UpdateTarget(this.lines); 
    });
  }
}
