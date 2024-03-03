using UnityEngine;
using UnityEditor;

public class BoardStorageTool : EditorWindow
{
    private BoardStorage selectedBoardStorage;

    [MenuItem("Tools/BoardStorage Tool")]
    private static void OpenWindow()
    {
        EditorWindow.GetWindow<BoardStorageTool>("BoardStorage Tool");
    }

    private void OnGUI()
    {
        GUILayout.Label("Select BoardStorage", EditorStyles.boldLabel);
        selectedBoardStorage = (BoardStorage)EditorGUILayout.ObjectField(selectedBoardStorage, typeof(BoardStorage), false);

        if (GUILayout.Button("Assign Board IDs to Puzzles"))
        {
            if (selectedBoardStorage != null)
            {
                AssignBoardIDs(); // selectedBoardStorage를 사용하여 BoardID 할당
            }
            else
            {
                Debug.LogError("No BoardStorage object selected.");
            }
        }
    }

    private void AssignBoardIDs()
    {
        if (selectedBoardStorage.boardArray == null) return;

        foreach (var board in selectedBoardStorage.boardArray)
        {
            BoardID id = board.boardID;
            var puzzleList = board.puzzleList;

            if (puzzleList != null)
            {
                foreach (var puzzle in puzzleList)
                {
                    Puzzle puzzleComponent = puzzle.GetComponent<Puzzle>();
                    if (puzzleComponent != null)
                    {
                        puzzleComponent.boardID = id; // Puzzle 컴포넌트의 boardID를 설정
                        EditorUtility.SetDirty(puzzleComponent); // 변경 사항을 저장
                    }
                }
            }
        }

        Debug.Log("Board IDs assigned to puzzles.");
    }
}
