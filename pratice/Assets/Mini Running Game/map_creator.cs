using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map_creator : MonoBehaviour
{
    public static float BLOCK_WIDTH = 1.0f;
    public static float BLOCK_HEIGHT = 1.0f;
    public static int BLOCK_NUM_IN_SCREEN = 24;
    private float timer = 0;
    private float counter = 0;

    private level_control level_ctrl = null;

    private struct FloorBlock
    {
        public bool is_created;
        public Vector3 position;
    };

    private FloorBlock last_block;
    private player_control player = null;
    private block_creator blc;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player_control>();
        last_block.is_created = false;
        blc = gameObject.GetComponent<block_creator>();

        level_ctrl = new level_control();
        level_ctrl.Initialize();
    }

    private void CreateFloorBlock()
    {
        Vector3 block_position;

        if (!last_block.is_created)
        {
            block_position = player.transform.position;
            block_position.x -= BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);
            block_position.y = 0.0f;
        }
        else
            block_position = last_block.position;

        block_position.x += BLOCK_WIDTH;

        //block.CreateBlock(block_position);
        level_ctrl.UpdateStatus();
        
        block_position.y = level_ctrl.current_block.height * BLOCK_HEIGHT;
        level_control.CreationInfo current = level_ctrl.current_block;
        
        if (current.block_type == block.TYPE.FLOOR)
            blc.CreateBlock(block_position);

        last_block.position = block_position;
        last_block.is_created = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        counter += Time.deltaTime;
        Debug.Log(level_control.level);
        if (counter <= 15)
            level_control.level = 1;
        else if (counter <= 30)
            level_control.level = 2;
        else if (counter <= 45)
            level_control.level = 3;
        else if (counter <= 60)
            level_control.level = 4;
        else if (counter <= 75)
            level_control.level = 5;
        else
            counter = 0;

        float block_generate_x = player.transform.position.x;

        block_generate_x += BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN + 1) / 2.0f;

        while (last_block.position.x < block_generate_x)
            CreateFloorBlock();
    }

    public bool IsGone(GameObject block_object)
    {
        bool result = false;

        float left_limit = player.transform.position.x - BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);

        if(block_object.transform.position.x < left_limit)
            result = true;

        return result;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2, 40, 256, 64), ((int)timer).ToString());
    }
}
