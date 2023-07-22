//
//  unity_facade.h
//  Defines API (methods) for unity apps
//
//  LICENSE_CODE ZON

#import <Foundation/Foundation.h>
NS_ASSUME_NONNULL_BEGIN
@interface brdsdk_facade : NSObject
@end
NS_ASSUME_NONNULL_END

#ifdef __cplusplus
extern "C" {
#endif
typedef void (*external_callback_function)(int choice);
void brdsdk_set_delegate(external_callback_function callback);
void brdsdk_init(char * _Nullable benefit,
                 int benefit_len,
                 char * _Nullable agree_btn,
                 int agree_btn_len,
                 char * _Nullable disagree_btn,
                 int disagree_btn_len,
                 bool skip_consent,
                 char * _Nullable language);
void brdsdk_opt_out(void);
void brdsdk_show_consent(char * _Nullable benefit,
                         int benefit_len,
                         char * _Nullable agree_btn,
                         int agree_btn_len,
                         char * _Nullable disagree_btn,
                         int disagree_btn_len,
                         char * _Nullable language);
int brdsdk_get_choice(void);
#ifdef __cplusplus
}
#endif
