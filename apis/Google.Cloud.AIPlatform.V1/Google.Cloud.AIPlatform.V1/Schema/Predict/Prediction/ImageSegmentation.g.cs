// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: google/cloud/aiplatform/v1/schema/predict/prediction/image_segmentation.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Google.Cloud.AIPlatform.V1.Schema.Predict.Prediction {

  /// <summary>Holder for reflection information generated from google/cloud/aiplatform/v1/schema/predict/prediction/image_segmentation.proto</summary>
  public static partial class ImageSegmentationReflection {

    #region Descriptor
    /// <summary>File descriptor for google/cloud/aiplatform/v1/schema/predict/prediction/image_segmentation.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ImageSegmentationReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Ck1nb29nbGUvY2xvdWQvYWlwbGF0Zm9ybS92MS9zY2hlbWEvcHJlZGljdC9w",
            "cmVkaWN0aW9uL2ltYWdlX3NlZ21lbnRhdGlvbi5wcm90bxI0Z29vZ2xlLmNs",
            "b3VkLmFpcGxhdGZvcm0udjEuc2NoZW1hLnByZWRpY3QucHJlZGljdGlvbiJT",
            "CiFJbWFnZVNlZ21lbnRhdGlvblByZWRpY3Rpb25SZXN1bHQSFQoNY2F0ZWdv",
            "cnlfbWFzaxgBIAEoCRIXCg9jb25maWRlbmNlX21hc2sYAiABKAlC6QIKOGNv",
            "bS5nb29nbGUuY2xvdWQuYWlwbGF0Zm9ybS52MS5zY2hlbWEucHJlZGljdC5w",
            "cmVkaWN0aW9uQiZJbWFnZVNlZ21lbnRhdGlvblByZWRpY3Rpb25SZXN1bHRQ",
            "cm90b1ABWlhjbG91ZC5nb29nbGUuY29tL2dvL2FpcGxhdGZvcm0vYXBpdjEv",
            "c2NoZW1hL3ByZWRpY3QvcHJlZGljdGlvbi9wcmVkaWN0aW9ucGI7cHJlZGlj",
            "dGlvbnBiqgI0R29vZ2xlLkNsb3VkLkFJUGxhdGZvcm0uVjEuU2NoZW1hLlBy",
            "ZWRpY3QuUHJlZGljdGlvbsoCNEdvb2dsZVxDbG91ZFxBSVBsYXRmb3JtXFYx",
            "XFNjaGVtYVxQcmVkaWN0XFByZWRpY3Rpb27qAjpHb29nbGU6OkNsb3VkOjpB",
            "SVBsYXRmb3JtOjpWMTo6U2NoZW1hOjpQcmVkaWN0OjpQcmVkaWN0aW9uYgZw",
            "cm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Google.Cloud.AIPlatform.V1.Schema.Predict.Prediction.ImageSegmentationPredictionResult), global::Google.Cloud.AIPlatform.V1.Schema.Predict.Prediction.ImageSegmentationPredictionResult.Parser, new[]{ "CategoryMask", "ConfidenceMask" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  /// Prediction output format for Image Segmentation.
  /// </summary>
  public sealed partial class ImageSegmentationPredictionResult : pb::IMessage<ImageSegmentationPredictionResult>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<ImageSegmentationPredictionResult> _parser = new pb::MessageParser<ImageSegmentationPredictionResult>(() => new ImageSegmentationPredictionResult());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<ImageSegmentationPredictionResult> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Cloud.AIPlatform.V1.Schema.Predict.Prediction.ImageSegmentationReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ImageSegmentationPredictionResult() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ImageSegmentationPredictionResult(ImageSegmentationPredictionResult other) : this() {
      categoryMask_ = other.categoryMask_;
      confidenceMask_ = other.confidenceMask_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ImageSegmentationPredictionResult Clone() {
      return new ImageSegmentationPredictionResult(this);
    }

    /// <summary>Field number for the "category_mask" field.</summary>
    public const int CategoryMaskFieldNumber = 1;
    private string categoryMask_ = "";
    /// <summary>
    /// A PNG image where each pixel in the mask represents the category in which
    /// the pixel in the original image was predicted to belong to. The size of
    /// this image will be the same as the original image. The mapping between the
    /// AnntoationSpec and the color can be found in model's metadata. The model
    /// will choose the most likely category and if none of the categories reach
    /// the confidence threshold, the pixel will be marked as background.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string CategoryMask {
      get { return categoryMask_; }
      set {
        categoryMask_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "confidence_mask" field.</summary>
    public const int ConfidenceMaskFieldNumber = 2;
    private string confidenceMask_ = "";
    /// <summary>
    /// A one channel image which is encoded as an 8bit lossless PNG. The size of
    /// the image will be the same as the original image. For a specific pixel,
    /// darker color means less confidence in correctness of the cateogry in the
    /// categoryMask for the corresponding pixel. Black means no confidence and
    /// white means complete confidence.
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string ConfidenceMask {
      get { return confidenceMask_; }
      set {
        confidenceMask_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as ImageSegmentationPredictionResult);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(ImageSegmentationPredictionResult other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (CategoryMask != other.CategoryMask) return false;
      if (ConfidenceMask != other.ConfidenceMask) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (CategoryMask.Length != 0) hash ^= CategoryMask.GetHashCode();
      if (ConfidenceMask.Length != 0) hash ^= ConfidenceMask.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (CategoryMask.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(CategoryMask);
      }
      if (ConfidenceMask.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(ConfidenceMask);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (CategoryMask.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(CategoryMask);
      }
      if (ConfidenceMask.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(ConfidenceMask);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (CategoryMask.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(CategoryMask);
      }
      if (ConfidenceMask.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(ConfidenceMask);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(ImageSegmentationPredictionResult other) {
      if (other == null) {
        return;
      }
      if (other.CategoryMask.Length != 0) {
        CategoryMask = other.CategoryMask;
      }
      if (other.ConfidenceMask.Length != 0) {
        ConfidenceMask = other.ConfidenceMask;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            CategoryMask = input.ReadString();
            break;
          }
          case 18: {
            ConfidenceMask = input.ReadString();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            CategoryMask = input.ReadString();
            break;
          }
          case 18: {
            ConfidenceMask = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
