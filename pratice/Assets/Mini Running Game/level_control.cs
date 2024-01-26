using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_control : MonoBehaviour
{
    public struct CreationInfo
    {
        public block.TYPE block_type;
        public int max_count;
        public float height;
        public int current_count;
    };

    public CreationInfo previous_block;
    public CreationInfo current_block;
    public CreationInfo next_block;

    public static int level = 0;

    private void ClearNextBlock(ref CreationInfo blk)
    {
        blk.block_type = block.TYPE.FLOOR;
        blk.max_count = 15;
        switch (level)
        {
            case 0:
            case 1:
                blk.height = 0;
                break;
            case 2:
                blk.height = Random.Range(-1.0f, 1.0f);
                break;
            case 3:
            case 4:
                blk.height = Random.Range(-2.0f, 2.0f);
                break;
            case 5:
                blk.height = Random.Range(-1.0f, 1.0f);
                break;
        }
        blk.current_count = 0;
    }

    public void Initialize()
    {
        ClearNextBlock(ref previous_block);
        ClearNextBlock(ref current_block);
        ClearNextBlock(ref next_block);
    }

    private void Updatelevel(ref CreationInfo current, CreationInfo previous)
    {
        switch (previous.block_type)
        {
            case block.TYPE.FLOOR:
                current.block_type = block.TYPE.HOLE;
                switch (level)
                {
                    case 0:
                    case 1:
                    case 2:
                        current.max_count = (int)Random.Range(1.0f, 3.0f);
                        break;
                    case 3:
                    case 4:
                        current.max_count = (int)Random.Range(2.0f, 4.0f);
                        break;
                    case 5:
                        current.max_count = (int)Random.Range(1.0f, 3.0f);
                        break;

                }
                current.height = previous.height;
                break;
            case block.TYPE.HOLE:
                current.block_type = block.TYPE.FLOOR;
                switch (level)
                {
                    case 0:
                    case 1:
                        current.max_count = (int)Random.Range(9.0f, 11.0f);
                        break;
                    case 2:
                        current.max_count = (int)Random.Range(8.0f, 10.0f);
                        break;
                    case 3:
                        current.max_count = (int)Random.Range(7.0f, 9.0f);
                        break;
                    case 4:
                        current.max_count = (int)Random.Range(5.0f, 7.0f);
                        break;
                    case 5:
                        current.max_count = (int)Random.Range(6.0f, 8.0f);
                        break;
                }
                break;
        }
    }

    public void UpdateStatus()
    {
        current_block.current_count++;

        if(current_block.current_count >= current_block.max_count)
        {
            previous_block = current_block;
            current_block = next_block;

            ClearNextBlock(ref next_block);
            Updatelevel(ref next_block, current_block);
        }
    }
}
