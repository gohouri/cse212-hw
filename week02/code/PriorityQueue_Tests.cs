using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue items with different priorities and dequeue them. Items should be dequeued in priority order.
    // Expected Result: Highest priority items are dequeued first, regardless of insertion order.
    // Defect(s) Found: (1) Loop condition was `index < _queue.Count - 1` which skipped the last element, so highest priority
    // item at the end was never found. (2) Used `>=` instead of `>` when comparing priorities, which broke FIFO for equal priorities.
    // (3) Item was not removed from queue after dequeuing - only the value was returned. 
    public void TestPriorityQueue_BasicPriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 3);
        priorityQueue.Enqueue("C", 2);
        priorityQueue.Enqueue("D", 5);
        
        // Should dequeue highest priority first
        Assert.AreEqual("D", priorityQueue.Dequeue(), "Highest priority (5) should be dequeued first");
        Assert.AreEqual("B", priorityQueue.Dequeue(), "Next highest priority (3) should be dequeued");
        Assert.AreEqual("C", priorityQueue.Dequeue(), "Next highest priority (2) should be dequeued");
        Assert.AreEqual("A", priorityQueue.Dequeue(), "Lowest priority (1) should be dequeued last");
    }

    [TestMethod]
    // Scenario: Enqueue items and verify that items are added to the back of the queue.
    // Expected Result: Items are enqueued to the back (FIFO order for same priority).
    // Defect(s) Found: Loop condition `index < _queue.Count - 1` skipped last element, and using `>=` instead of `>`
    // caused later items to be selected over earlier ones when priorities were equal. 
    public void TestPriorityQueue_EnqueueToBack()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 1);
        priorityQueue.Enqueue("C", 1);
        
        // All have same priority, so should follow FIFO (first in, first out)
        Assert.AreEqual("A", priorityQueue.Dequeue(), "First item enqueued should be dequeued first when priorities are equal");
        Assert.AreEqual("B", priorityQueue.Dequeue(), "Second item enqueued should be dequeued second when priorities are equal");
        Assert.AreEqual("C", priorityQueue.Dequeue(), "Third item enqueued should be dequeued third when priorities are equal");
    }

    [TestMethod]
    // Scenario: Multiple items with the same highest priority. Should dequeue the one closest to the front (FIFO).
    // Expected Result: When multiple items have the same highest priority, the first one added (closest to front) is dequeued.
    // Defect(s) Found: Using `>=` instead of `>` meant that when priorities were equal, the last item found was selected
    // instead of the first one, violating FIFO requirement for same priority items. 
    public void TestPriorityQueue_SamePriorityFIFO()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 5);
        priorityQueue.Enqueue("B", 3);
        priorityQueue.Enqueue("C", 5);  // Same priority as A
        priorityQueue.Enqueue("D", 2);
        priorityQueue.Enqueue("E", 5);   // Same priority as A and C
        
        // A, C, and E all have priority 5. A was added first, so it should be dequeued first
        Assert.AreEqual("A", priorityQueue.Dequeue(), "When multiple items have same highest priority, first one added should be dequeued first");
        Assert.AreEqual("C", priorityQueue.Dequeue(), "Next item with highest priority should be dequeued");
        Assert.AreEqual("E", priorityQueue.Dequeue(), "Last item with highest priority should be dequeued");
        Assert.AreEqual("B", priorityQueue.Dequeue(), "Items with lower priority should be dequeued after higher priority items");
        Assert.AreEqual("D", priorityQueue.Dequeue(), "Lowest priority item should be dequeued last");
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty queue.
    // Expected Result: InvalidOperationException should be thrown with message "The queue is empty."
    // Defect(s) Found: None - this test passes. The exception handling is already correctly implemented. 
    public void TestPriorityQueue_EmptyQueue()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown when dequeuing from empty queue.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message, "Exception message should match expected message");
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                 string.Format("Unexpected exception of type {0} caught: {1}",
                                e.GetType(), e.Message)
            );
        }
    }

    [TestMethod]
    // Scenario: Mix of priorities with some duplicates, verify correct order.
    // Expected Result: Items dequeued in priority order, with FIFO for same priorities.
    // Defect(s) Found: (1) Loop condition skipped last element. (2) Using `>=` instead of `>` broke FIFO for same priorities.
    // (3) Items were not removed from queue after dequeuing. 
    public void TestPriorityQueue_MixedPriorities()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 1);
        priorityQueue.Enqueue("High1", 10);
        priorityQueue.Enqueue("Mid1", 5);
        priorityQueue.Enqueue("High2", 10);  // Same priority as High1, but added later
        priorityQueue.Enqueue("Mid2", 5);     // Same priority as Mid1, but added later
        priorityQueue.Enqueue("Low", 1);      // Same priority as First, but added later
        
        // Should dequeue: High1, High2 (in FIFO order), then Mid1, Mid2, then First, Low
        Assert.AreEqual("High1", priorityQueue.Dequeue(), "First highest priority item should be dequeued first");
        Assert.AreEqual("High2", priorityQueue.Dequeue(), "Second highest priority item should be dequeued second");
        Assert.AreEqual("Mid1", priorityQueue.Dequeue(), "First medium priority item should be dequeued third");
        Assert.AreEqual("Mid2", priorityQueue.Dequeue(), "Second medium priority item should be dequeued fourth");
        Assert.AreEqual("First", priorityQueue.Dequeue(), "First low priority item should be dequeued fifth");
        Assert.AreEqual("Low", priorityQueue.Dequeue(), "Second low priority item should be dequeued last");
    }
}